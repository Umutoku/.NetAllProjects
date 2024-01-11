// See https://aka.ms/new-console-template for more information
using System.Data;
using AutoMapper.QueryableExtensions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using UdemyEFCore.CodeFirst;
using UdemyEFCore.CodeFirst.DAL;
using UdemyEFCore.CodeFirst.DTOs;
using UdemyEFCore.CodeFirst.Mappers;

Initializer.Build();
// using(var _context = new AppDbContext(123)) //contracter ile tenant ayrımı



using (var _context = new AppDbContext())
{
    //view üzerinden veri çekme
    var product = _context.ProductFulls.ToList();

    List<Person> GetPeople(int page = 1, int pageSize = 10)
    {
        return _context.People.Skip((page - 1) * pageSize).Take(pageSize).ToList();
    }

    var id = 5;
    //Raw sql ile veri çekme
    var products = _context.Products.FromSqlRaw("select * from Products where id={0}", id).ToList();

    //Interpolated ile veri çekme
    var productss = _context.Products.FromSqlInterpolated($"select * from Products where id={id}").ToList();

    //ReadUncommitted
    using(var transaction = _context.Database.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted))
    {
        var productT = _context.Products.First();
        _context.SaveChanges();
        transaction.Commit();
    }
    //readCommitted
    using(var transaction = _context.Database.BeginTransaction(System.Data.IsolationLevel.ReadCommitted))
    {
        var productT = _context.Products.First();
        _context.SaveChanges();
        transaction.Commit();
    }
    //repetable
    using(var transaction = _context.Database.BeginTransaction(System.Data.IsolationLevel.RepeatableRead))
    {
        var productT = _context.Products.First();
        _context.SaveChanges();
        var productT2 = _context.Products.First(); //aynı data gelir

        transaction.Commit();
    }

    //repetable
    using(var transaction = _context.Database.BeginTransaction(System.Data.IsolationLevel.Serializable))
    {
        var productT = _context.Products.First();
        _context.SaveChanges();
        var productT2 = _context.Products.First(); //aynı data gelir
        //repetabledan farklı olarak insert işlemi de yapılamaz

        transaction.Commit();
    }

    //repetable
    using(var transaction = _context.Database.BeginTransaction(System.Data.IsolationLevel.Snapshot))
    {
        var productT = _context.Products.First();
        _context.SaveChanges();
        var productT2 = _context.Products.First(); //aynı data gelir
        //repetabledan farklı olarak update insert delete yapabilir başka transaction ama burada gözükmez

        transaction.Commit();
    }

    //Select içerisinde field alanlarını çekmek için null karşılığı olmayan tüm veriyi çekiyoruz
    //Bu yüzden yeni bir entity oluşturduk ama hala products üzerinden çekiyoruz
    var productEssential = _context.ProductEssentials.FromSqlRaw("select Id,Name,Price from Products").ToList();

    //Left join
    var result3 = (from pf in _context.ProductFeatures
                   join p in _context.Products on pf.Id equals p.Id into g //left join işlemi
                   from p in g.DefaultIfEmpty()
                   select new
                   {
                       ProductName = p.Name,
                       ProductColor = pf.Color,
                       ProductWidth = (int?)pf.Width == null ? 5 : pf.Width //null olabilir
                   }).ToList();

    //Right join
    var result4 = (from p in _context.Products
                   join pf in _context.ProductFeatures on p.Id equals pf.Id into g //Right join işlemi, üstekinin tersi
                   from pf in g.DefaultIfEmpty()
                   select new
                   {
                       ProductName = p.Name,
                       ProductColor = pf.Color,
                       ProductWidth = (int?)pf.Width == null ? 5 : pf.Width //null olabilir
                   }).ToList();



    //Inner join LinQ
    var result = _context.Categories.Join(_context.Products, c => c.Id, p => p.CategoryId, (c, p) => new { c, p })
                                    .Join(_context.ProductFeatures, x => x.p.Id, y => y.Id, (c, pf) => new
                                    {
                                        ProductName = c.p.Name,
                                        ProductColor = pf.Color,
                                        CategoryName = c.c.Name
                                    }).ToList();

    var outerJoin = result4.Union(result3); //union ile birleştirme işlemi


    var result2 = (from c in _context.Categories
                   join p in _context.Products on c.Id equals p.CategoryId
                   join pf in _context.ProductFeatures on p.Id equals pf.Id
                   select new
                   {
                       ProductColor = pf.Color,
                       CategoryName = c.Name,
                       ProductName = p.Name
                   }).ToList();


    _context.People.Add(new Person { Name = "Ahmet", Phone = "123" });
    _context.People.Add(new Person { Name = "Mehmet", Phone = "123456" });

    _context.SaveChanges();


    var productsss = _context.Products.IgnoreQueryFilters().ToList(); // silinmiş ürünleri de çekmek için|

    // TagWith ile sql sorgusunu görebiliriz
    var productsWithFeature = _context.Products
        .TagWith("Bu query ürünler ve özellikleri getirir").Include(x => x.ProductFeature).ToList(); //eager loading

    var productsWithFeature2 = _context.Products.First(x => x.Id == 1); //lazy loading

    _context.Entry(productsWithFeature2).Reference(x => x.ProductFeature).Load(); //lazy loading
    _context.Entry(productsWithFeature).State = EntityState.Modified; //state değişimi //AsNoTracking var ise bildirmek gerekli

    await _context.Products.FromSqlRaw("exec sp_get_products").ToListAsync(); //stored procedure ile veri çekme

    await _context.ProductFulls.FromSqlRaw("exec sp_get_products_full").ToListAsync(); //stored procedure ile veri çekme

    int categoryId = 1;

    decimal price = 50;

    await _context.ProductFulls
        .FromSqlInterpolated($"exec sp_get_products_full_by_parameters {categoryId},{price}").ToListAsync(); //stored procedure ile veri çekme

    _context.Database.ExecuteSqlRaw("exec sp_get_products_full_by_parameters {0},{1}", categoryId, price); //stored procedure ile veri çekme

    _context.Database.ExecuteSqlInterpolated($"exec sp_get_products_full_by_parameters {categoryId},{price}"); //stored procedure ile veri çekme

    var spProduct = new ProductFull { Name = "Kalem", Price = 100 };

    var newProductId = new SqlParameter("@newProductId", System.Data.SqlDbType.Int) { Direction = ParameterDirection.Output }; //output parametresi için

    _context.productFulls.FromSqlInterpolated($"exec sp_insert_product {spProduct.Name},{spProduct.Price},{spProduct},{spProduct},{spProduct},{spProduct},{spProduct}, {newProductId} out"); //stored procedure ile veri çekme

    var prodctId = newProductId.Value; //output parametresi için

    var resultt = await _context.ProductFulls.ToListAsync(); //Function procedure ile veri çekme

    categoryId = 1;

    var productsFn = await _context.ProductFulls.FromSqlInterpolated($"select * from GetProductFulls({categoryId})").ToListAsync(); //Function procedure ile veri çekme

    var productfn = _context.GetProductWithFeatures(1).Where(x => x.Product.Name != "").ToList(); // sql funtion with dbContext

    //var count = _context.GetProductCount(1); // tek satırda kullanılamaz, mutlaka bir ef core içerisinde kullanmalıyız.

    var categories = _context.Categories.Select(x => new
    {
        CategoryName = x.Name,
        ProductCount = _context.GetProductCount(x.Id)
    });

    //Bu şekilde scaeler function bağımsız bir şekilde kullanılabilir // as Count tarafındaki isimlendirme sınıf içerisindeki propla aynı olmalı
    var productCount = _context.ProductCounts.FromSqlInterpolated($"select dbo.fc_get_product_count({categoryId}) as Count ").First().Count; // sql function

    var prodcutsProjection = _context.Products.Include(x => x.ProductFeature).ToList(); // eager loading. Projection işlemi..

    var productAnon = _context.Products.Include(x => x.Category).Include(x => x.ProductFeature).Select(x => new
    {
        CategoryName = x.Category.Name,
        ProductName = x.Name,
        ProductPrice = x.Price
    }).Where(x => x.CategoryName == ""); // select quearayle döndüğü için where sorgusu sql cümleciğine eklenir

    var productDto = _context.Products.Include(x => x.Category).Include(x => x.ProductFeature).Select(x => new ProductDto
    {
        CategoryName = x.Category.Name,
        ProductName = x.Name,
        ProductPrice = x.Price
    }).Where(x => x.CategoryName == "").ToList();

    var productDtoWithMapper = ObjectMapper.Mapper.Map<List<ProductDto>>(productDto);

    // dtoya gelmeden önce bütün productsın çekilmesini engeller. sadece dtoya göre veri çeker
    var productDtoWithProject = _context.Products.ProjectTo<ProductDto>(ObjectMapper.Mapper.ConfigurationProvider).ToList();

    // select ile çektiğimizde navigation propertyler ekli ise include ve theninclude ihtiyacımız yok

    var categoriesWithProducts = _context.Categories
    //.Include(x=>x.Products).ThenInclude(x=>x.ProductFeature)
    .Select(x => new
    {
        CategoryName = x.Name,
        Products = String.Join(",", x.Products.Select(z => z.Name)), //kalem1, kalem2,
        TotalPrice = x.Products.Sum(x => x.Price),
        TotalWidth = (int?)x.Products.Select(x => x.ProductFeature.Width).Sum() // nul olabilir. Aggregation işlemlerinde boş değerleri atlasın
    }).Where(x => x.TotalPrice > 100).OrderBy(x => x.TotalPrice).ToList(); // tolist'e kadar olan kısım sql cümleciğine dönüşecek

    using (var transaction = _context.Database.BeginTransaction()) // transaction scope'u ile işlem devamlılığı kontrol altına alınabilir
    {
        var category = new Category() { Name = "telefonlar" };

        _context.Categories.Add(category);

        var productsSave = new Product()
        {
            Name = "Transaction ",
            Price = 100,
            Barcode = 100,
            CategoryId = category.Id,
        };

        _context.Products.Add(productsSave);

        _context.SaveChanges();

        transaction.Commit();
    }



    //functionlarda where ifadesi kullanılabilir ama store procedureda kullanılamaz.
    // çünkü functionlar select içinde kullanılabilir

    ////local function içinde kullanılamaz
    //var persons = _context.People.Where(x=> formatPhone(x.Phone)=="554545").ToList();

    ////data serverdan çekildikten sonra method çalışır, tolist ile çekilir
    //var personsTo = _context.People.ToList().Where(x => formatPhone(x.Phone) == "554545").ToList();

    ////tolist sonrası isimsiz sınıf ile method kullanarak formatlı telefon çekildi
    //var person = _context.People.ToList().Select(x=> new { x.Name, Phone = formatPhone(x.Phone) }).ToList();


    //Console.WriteLine("işlem bitti.");

    //string formatPhone(string phone)
    //{
    //    return phone.Substring(0, 3) + "****" + phone.Substring(7);
    //}

    ////sql sorgusu çekip mapleme işlemi
    //var productFulls = _context.ProductFulls.FromSqlRaw(@"select p.Id 'Product_Id', c.Name 'CategoryName', p.Name, p.price, pf.Height from Products p join productFeatures pf on p.Id = pf.Id join Categories c on p.CategoryId=c.Id").ToList();



    //var category = _context.Categories.First();
    //Console.WriteLine("Lazy loading için product ayrıldı, sadece category çekilecek");
    //var product = category.Products.First();

    // aradaki işlemlerde veri ihtiyacımız yok ise
    // Eager loading ile veriyi daha sonra çekiyoruz

    //if(true)
    //{
    //    //explicit loading
    //    //ihtiyacımız olduğunda veriyi daha sonra çektik
    //    _context.Entry(category).Collection(x => x.Products).Load();
    //    //birebir ilişki olduğu için referans tipi ile çekiyoruz
    //    _context.Entry(product).Reference(x => x.ProductFeature).Load();
    //}



    // Include ile ilişkili tabloları çekebiliriz ve eager loading yaparız //ThenInclude ile products içindekileri çekiyoruz
    //_context.Categories.Include(x => x.Products).ThenInclude(x=>x.ProductFeature);

    //var student = new Student() { Name = "A" ,Age= 25};
    //student.Teachers.Add(new Teacher() { Name = "Ahmet" });
    //student.Teachers.Add(new Teacher() { Name = "Mehmet" });

    //_context.Add(student);
    //_context.SaveChanges();

    //var teacher = new Teacher() { Name = "A", Students = new() { new() { Age = 10 ,Name="Hasan"} } };

    //Product -> parent productFeature -> child
    //var product = new Product { Name = "Silgi", Price = 120, Barcode = 123 ,Stock=100,
    //    Category= _context.Categories.Any(x=>x.Name == "Silgi") ? _context.Categories.First(x => x.Name == "Silgi") :
    //    new Category { Name="Silgi"},
    //ProductFeature = new ProductFeature {Color="Red",Height=100,Width =200}
    //};



    //var category = new Category() { Name = "Test" };
    //var product = new Product() { Name ="kalem 1", Price=100, Stock=200, Barcode=324, Category = category };
    //category.Products.Add(new Product() { Name = "kalem 1", Price = 100, Stock = 200, Barcode = 324, Category = category });
    //category.Products.AddRange // bu şekilde liste eklenebilir


    //_context.Categories.Add(category); // Buna gerek yok 
    //_context.Products.Add(product); // içerisinde category olduğu için onu da dahil eder //navigation property


    //_context.Products.Add(new() { Name = "Kalem", Price = 200, Stock = 50 });
    //_context.Products.Add(new() { Name = "Kalem 2", Price = 100, Stock = 60 });
    //_context.Products.Add(new() { Name = "Kalem 3", Price = 300, Stock = 90 });

    //var product1 = _context.Products.First();
    //var product2 = _context.Products.FirstOrDefault(x=>x.Id == 1,new Product { Name="Default value" , Price=100, Stock =200});
    ////Id ile arama yapacak isek en uygun find methodu
    //var product3 = _context.Products.FindAsync(10,5); // birden fazla primary key varsa 



    //product1.Price = 500;

    //Track edilen datada modified işaretleniyor

    //_context.Update(product1); // Eğer data çekildi ise tekrar bunu yapmaya gerek yok

    //_context.Update(new Product { Name = "kalemmm" }); //Bu şekilde çekilmemiş datalarda update olabilir

    //_context.SaveChanges(); 


    //_context.ContextId //Birden fazla veritabanı varsa id atama ya da kontrol etmek için

    //var newProduct = new Product { Name = "Kalemd", Price = 100, Stock = 5};

    //await _context.Products.AddAsync(newProduct); //alt satırdaki kod ile aynı işi yapar

    //_context.Entry(newProduct).State = EntityState.Added;
    //Console.WriteLine($"State değişimi: { _context.Entry(newProduct).State}");

    //var products = _context.Products.AsNoTracking().ToListAsync(); //eğer değişiklik yapılmayacaksa direk tolist yük olur. AsNoTracking eklenmeli

    //var state = _context.Entry(products).State; // Her datanın stateini atar.


}
