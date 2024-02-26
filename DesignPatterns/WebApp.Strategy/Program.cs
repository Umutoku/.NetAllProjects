using BaseProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using WebApp.Strategy.Models;
using WebApp.Strategy.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//builder.Services.AddScoped<IProductRepository, ProductRepositoryFromSqlServer>(); // IProductRepository arayüzüne karşılık ProductRepositoryFromSqlServer sınıfı kullanılmayacak. runtime sırasında karar verilecek.

builder.Services.AddHttpContextAccessor(); // sayesinde HttpContextAccessor sınıfı üzerinden HTTP isteği bilgilerine erişilebilir

builder.Services.AddScoped<IProductRepository>(serviceProvider =>
{
    var httpContextAccessor = serviceProvider.GetRequiredService<IHttpContextAccessor>(); // sayesinde HttpContextAccessor sınıfı üzerinden HTTP isteği bilgilerine erişilebilir

    var claim = httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == Settings.claimDatabaseType); // sayesinde claim'den database type alınır.

    var context = serviceProvider.GetRequiredService<AppIdentityDbContext>(); // sayesinde AppIdentityDbContext sınıfı üzerinden veritabanı işlemleri gerçekleştirilebilir

    if(claim == null)
    {
        return new ProductRepositoryFromSqlServer(context); // eğer claim yoksa varsayılan olarak SQL Server veritabanı kullanılır
    }
    else
    {
        var databaseType = (EDatabaseType) int.Parse(claim.Value); // sayesinde claim'den database type alınır.

        if(databaseType == EDatabaseType.SqlServer)
        {
            return new ProductRepositoryFromSqlServer(context); // eğer database type SQL Server ise SQL Server veritabanı kullanılır
        }
        else
        {
            return new ProductRepositoryFromMongoDb(configuration:builder.Configuration); // eğer database type MongoDB ise MongoDB veritabanı kullanılır
        }
    }
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
