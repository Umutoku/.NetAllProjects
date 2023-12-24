// See https://aka.ms/new-console-template for more information
using Microsoft.EntityFrameworkCore;
using UdemyEFCore.CodeFirst;
using UdemyEFCore.CodeFirst.DAL;

Initializer.Build();

using(var _context = new AppDbContext())
{
    //view üzerinden veri çekme
    var product = _context.ProductFulls.ToList();



    var id = 5;
    //Raw sql ile veri çekme
    var products = _context.Products.FromSqlRaw("select * from Products where id={0}",id).ToList();
    
    //Interpolated ile veri çekme
    var productss = _context.Products.FromSqlInterpolated($"select * from Products where id={id}").ToList();




    //Select içerisinde field alanlarını çekmek için null karşılığı olmayan tüm veriyi çekiyoruz
    //Bu yüzden yeni bir entity oluşturduk ama hala products üzerinden çekiyoruz
    var productEssential = _context.ProductEssentials.FromSqlRaw("select Id,Name,Price from Products").ToList();

    //Left join
    var result3 = (from pf in _context.ProductFeatures
                  join p in _context.Products on pf.Id equals p.Id into g //left join işlemi
                  from p in g.DefaultIfEmpty()
                  select new { 
                      ProductName = p.Name,
                      ProductColor = pf.Color,
                      ProductWidth = (int?)pf.Width== null? 5 : pf.Width //null olabilir
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
    var result = _context.Categories.Join(_context.Products, c => c.Id, p => p.CategoryId, (c, p) => new {c,p})
                                    .Join(_context.ProductFeatures,x=>x.p.Id,y=>y.Id,(c,pf) => new {
                                    ProductName = c.p.Name,
                                    ProductColor = pf.Color,
                                    CategoryName  = c.c.Name
                                    }).ToList();
                                    
    var outerJoin =result4.Union(result3); //union ile birleştirme işlemi


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