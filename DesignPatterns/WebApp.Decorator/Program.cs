using WebApp.Decorator.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApp.Decorator.Repositories;
using Microsoft.Extensions.Caching.Memory;
using WebApp.Decorator.Repositories.Decorator;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddMemoryCache(); // sayesinde IMemoryCache arayüzüne karşılık gelen MemoryCache sınıfı kullanılacak
builder.Services.AddHttpContextAccessor(); // sayesinde IHttpContextAccessor arayüzüne karşılık gelen HttpContextAccessor sınıfı kullanılacak. Bu sayede HttpContext.Current gibi özelliklere erişim sağlanabilir

//builder.Services.AddScoped<IProductRepository, ProductRepository>(); // IProductRepository arayüzüne karşılık gelen ProductRepository sınıfı kullanılacak

// 1. yol
//builder.Services.AddScoped<IProductRepository>(sp =>
//{
//    var context = sp.GetRequiredService<AppIdentityDbContext>(); // AppIdentityDbContext sınıfı kullanılarak veritabanı işlemleri gerçekleştirilecek
//    var memoryCache = sp.GetRequiredService<IMemoryCache>(); // IMemoryCache arayüzüne karşılık gelen MemoryCache sınıfı kullanılacak
//    var productRepository = new ProductRepository(context); // ProductRepository sınıfı kullanılarak veritabanı işlemleri gerçekleştirilecek
//    var logProvider = sp.GetRequiredService<ILogger<ProductRepositoryLoggingDecorator>>(); // ILoggerProvider sınıfı kullanılarak loglama işlemleri gerçekleştirilecek
//    var cacheDecorator = new ProductRepositoryCacheDecorator(productRepository, memoryCache); // ProductRepositoryCacheDecorator sınıfı kullanılarak önbellek işlemleri gerçekleştirilecek

//    var logDecorator = new ProductRepositoryLoggingDecorator(cacheDecorator,logProvider); // ProductRepositoryLoggingDecorator sınıfı kullanılarak loglama işlemleri gerçekleştirilecek

//    return logDecorator;
//});

// 2. yol
//builder.Services.AddScoped<IProductRepository, ProductRepository>().Decorate<IProductRepository, ProductRepositoryCacheDecorator>().Decorate<IProductRepository, ProductRepositoryLoggingDecorator>(); // Decorate metodu sayesinde ProductRepository sınıfı üzerine ProductRepositoryCacheDecorator ve ProductRepositoryLoggingDecorator sınıfları uygulanır

// 3. yol runtime 

builder.Services.AddScoped<IProductRepository>(sp =>
{
    var httpContextAccessor = sp.GetRequiredService<IHttpContextAccessor>(); // IHttpContextAccessor arayüzüne karşılık gelen HttpContextAccessor sınıfı kullanılacak
    var context = sp.GetRequiredService<AppIdentityDbContext>(); // AppIdentityDbContext sınıfı kullanılarak veritabanı işlemleri gerçekleştirilecek
    var memoryCache = sp.GetRequiredService<IMemoryCache>(); // IMemoryCache arayüzüne karşılık gelen MemoryCache sınıfı kullanılacak
    var logProvider = sp.GetRequiredService<ILogger<ProductRepositoryLoggingDecorator>>(); // ILoggerProvider sınıfı kullanılarak loglama işlemleri gerçekleştirilecek
    var productRepository = new ProductRepository(context); // ProductRepository sınıfı kullanılarak veritabanı işlemleri gerçekleştirilecek

    if(httpContextAccessor.HttpContext.User.Identity.Name=="user1")
    {
        var cacheDecorator = new ProductRepositoryCacheDecorator(productRepository, memoryCache); // ProductRepositoryCacheDecorator sınıfı kullanılarak önbellek işlemleri gerçekleştirilecek
        return cacheDecorator;
    }

    var logDecorator = new ProductRepositoryLoggingDecorator(productRepository, logProvider); // ProductRepositoryLoggingDecorator sınıfı kullanılarak loglama işlemleri gerçekleştirilecek

    return logDecorator;

});


builder.Services.AddDbContext<AppIdentityDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.User.RequireUniqueEmail = true; // Email must be unique
}
).AddEntityFrameworkStores<AppIdentityDbContext>(); // IdentityUser ve IdentityRole sınıflarını kullanarak kimlik doğrulama işlemlerini gerçekleştirecek

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

using var scope = app.Services.CreateScope();

var appIdentityDbContext = scope.ServiceProvider.GetRequiredService<AppIdentityDbContext>(); // sayesinde AppIdentityDbContext sınıfı üzerinden veritabanı işlemleri gerçekleştirilebilir

var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>(); // sayesinde kullanıcı işlemleri gerçekleştirilebilir

appIdentityDbContext.Database.Migrate(); // veritabanı yoksa oluşturulur, varsa güncellenir

if (!userManager.Users.Any())
{
    userManager.CreateAsync(new AppUser() {UserName = "admin", Email = "admin@outlook.com"},"Pass1234!").Wait();
    userManager.CreateAsync(new AppUser() { UserName = "user", Email = "user@outlook.com"}, "Pass1234!").Wait();
    userManager.CreateAsync(new AppUser() { UserName = "user2", Email = "user2@outlook.com"}, "Pass1234!").Wait();
    // Eğer kullanıcılar yoksa yukarıdaki gibi kullanıcılar oluşturulur
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication(); // sayesinde kimlik doğrulama işlemleri gerçekleştirilebilir
app.UseAuthorization(); // sayesinde yetkilendirme işlemleri gerçekleştirilebilir

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
