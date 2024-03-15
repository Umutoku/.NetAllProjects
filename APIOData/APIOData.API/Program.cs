using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.ModelBuilder;
using OData.API.Models;
using UdemyAPIOData.API.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var modelBuilder = new ODataConventionModelBuilder();


// .../odata/Categories(1)/Products
modelBuilder.EntityType<Product>().Action("TotalPrice").Returns<decimal>(); // Product entity'sine TotalPrice adında bir action ekler ve geriye decimal tipinde bir değer döner.

// .../odata/Categories/GetProducts 
modelBuilder.EntityType<Product>().Collection.Action("TotalPrice").Returns<decimal>(); // Product entity'sine TotalPrice adında bir action ekler ve geriye decimal tipinde bir değer döner.

// .../odata/Categories/TotalProductprice 
modelBuilder.EntityType<Category>().Collection.Function("TotalProductPrice").Returns<decimal>().Parameter<int>("categoryId"); // Category entity'sine TotalProductPrice adında bir function ekler ve geriye decimal tipinde bir değer döner. Ayrıca categoryId adında bir parametre alır.

var actionTotalPrice = modelBuilder.EntityType<Product>().Action("TotalPrice"); // Product entity'sine TotalPrice adında bir action ekler.

actionTotalPrice.Parameter<int>("productId"); // TotalPrice action'ına productId adında bir parametre ekler.
actionTotalPrice.Returns<decimal>(); // TotalPrice action'ından geriye decimal tipinde bir değer döner.
actionTotalPrice.Parameter<int>("count"); // TotalPrice action'ına count adında bir parametre ekler.

modelBuilder.EntityType<Product>().Collection.Action("Login").Returns<string>().Parameter<Login>("login"); // Product entity'sine Login adında bir action ekler ve geriye string tipinde bir değer döner. Ayrıca login adında bir parametre alır.

///////Function
///

modelBuilder.EntityType<Category>().Collection.Function("CategoryCount").Returns<int>(); // Category entity'sine CategoryCount adında bir function ekler ve geriye int tipinde bir değer döner.

var multiplyFunc = modelBuilder.EntityType<Product>().Collection.Function("MultiplyFunc").Returns<int>(); // Product entity'sine MultiplyFunc adında bir function ekler ve geriye int tipinde bir değer döner.

multiplyFunc.Parameter<int>("number1"); // MultiplyFunc function'ına number1 adında bir parametre ekler.
multiplyFunc.Parameter<int>("number2"); // MultiplyFunc function'ına number2 adında bir parametre ekler.

modelBuilder.EntityType<Product>().Function("Tax").Returns<double>().Parameter<double>("kdv"); // Product entity'sine Tax adında bir function ekler ve geriye double tipinde bir değer döner. Ayrıca kdv adında bir parametre alır.

modelBuilder.Function("GetKDV").Returns<int>(); // GetKDV adında bir function ekler ve geriye int tipinde bir değer döner.


modelBuilder.EntitySet<Category>("Categories"); // Category entity'sini OData modeline ekler.
modelBuilder.EntitySet<Product>("Products"); // Product entity'sini OData modeline ekler.

builder.Services.AddControllers().AddOData(
    options => options
    .Select() // OData sorgularında select ile sadece belirli alanları getirebilmek için kullanılır.
    .Filter() // OData sorgularında filtreleme işlemlerini kullanabilmek için kullanılır.
    .OrderBy()
    .Expand() // OData sorgularında expand ile ilişkili tabloları getirebilmek için kullanılır.
    .Count() // OData sorgularında count ile toplam kayıt sayısını getirebilmek için kullanılır.
    .SetMaxTop(null) // OData sorgularında max top ile getirilecek kayıt sayısını sınırlamak için kullanılır.
    .SkipToken() // OData sorgularında skip token ile sayfalama işlemlerini kullanabilmek için kullanılır.
    .AddRouteComponents(
        "odata", // OData sorguları için kullanılacak route adı.
        modelBuilder.GetEdmModel())); // OData sorgularını kullanabilmek için gerekli ayarlamalar yapılır.

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));   
    opt.EnableSensitiveDataLogging(); // Veritabanı sorgularını loglamak için kullanılır.
    opt.EnableDetailedErrors(); // Hata detaylarını görmek için kullanılır.
    opt.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking); // Sorgu takibini kapatmak için kullanılır.
    opt.LogTo(Console.WriteLine); // Veritabanı sorgularını konsola loglamak için kullanılır.

});

builder.Services.AddODataQueryFilter(); // OData sorgularını filtrelemek için kullanılır.

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting(); // Yönlendirme işlemleri için kullanılır.

app.UseEndpoints(endpoints => endpoints.MapControllers()); // Controller sınıflarını yönlendirmek için kullanılır.

app.UseHttpsRedirection();

app.UseAuthorization();


app.Run();
