using WebApp.Observer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApp.Observer.Observer;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();



//builder.Services.AddSingleton<UserObserverSubject>(sp =>
//{
//    UserObserverSubject subject = new UserObserverSubject();

//    subject.Attach(new UserObserverCreateDiscount(sp)); // Kullanıcı oluşturulduğunda indirim oluşturulması için gerekli olan observer'ı ekledik

//    subject.Attach(new UserObserverSendEmail(sp)); // Kullanıcı oluşturulduğunda email gönderilmesi için gerekli olan observer'ı ekledik

//    subject.Attach(new UserObserverWriteToConsole(sp)); // Kullanıcı oluşturulduğunda konsola yazdırılması için gerekli olan observer'ı ekledik

//    return subject;
//}); // Observer pattern için gerekli olan sınıfı ekledik

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly)); // MediatR kütüphanesini kullanabilmek için gerekli olan servisi ekledik

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
