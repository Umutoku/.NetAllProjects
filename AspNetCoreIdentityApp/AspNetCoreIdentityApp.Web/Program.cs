using AspNetCoreIdentityApp.Web.Models;
using Microsoft.EntityFrameworkCore;
using AspNetCoreIdentityApp.Web.Extensions;
using Microsoft.AspNetCore.Identity;
using AspNetCoreIdentityApp.Web.OptionsModels;
using AspNetCoreIdentityApp.Web.Services;
using System.ComponentModel.DataAnnotations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.Configure<SecurityStampValidatorOptions>(options =>
{
    options.ValidationInterval  = TimeSpan.FromSeconds(5000); // sayesinde kullanıcıların güvenlik damgalarının ne sıklıkla kontrol edileceğini belirler
});

builder.Services.AddScoped<IEmailService, EmailService>(); // IEmailService ve EmailService sınıflarını ekler

builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings")); // appsettings.json dosyasındaki EmailSettings alanındaki değerleri alır

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));

builder.Services.Configure<DataProtectionTokenProviderOptions>(options =>
{
    options.TokenLifespan = TimeSpan.FromHours(5); // Token'ın süresini belirler
});

builder.Services.AddIdentityWithExt(); // AddIdentityWithExt metodu çağrılır

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Home/SignIn";
    options.LogoutPath = "/Member/logout";
    options.AccessDeniedPath = "/Home/AccessDenied"; // Yetkisiz erişim durumunda yönlendirilecek sayfa
    options.SlidingExpiration = true; // Kullanıcı tekrar giriş yaptığında cookie süresini uzatır
    options.ExpireTimeSpan = TimeSpan.FromDays(30);
    options.ReturnUrlParameter = "returnUrl"; // Kullanıcı giriş yaptıktan sonra yönlendirilecek sayfa
    options.Cookie = new CookieBuilder
    {
        HttpOnly = false,
        Name = ".AspNetCore.Identity.Cookie",
        SameSite = SameSiteMode.Lax, // Cookie'nin güvenliği için kullanılır
        SecurePolicy = CookieSecurePolicy.SameAsRequest // Cookie'nin güvenliği için kullanılır
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();


    app.MapControllerRoute( // Areas için kullanılır
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
