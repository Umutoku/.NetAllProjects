// See https://aka.ms/new-console-template for more information
using Microsoft.EntityFrameworkCore;
using UdemyEFCore.CodeFirst;
using UdemyEFCore.CodeFirst.DAL;

Initializer.Build();

using(var _context = new AppDbContext())
{




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