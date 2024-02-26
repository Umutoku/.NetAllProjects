using WebApp.Composite.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

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
    var newUser = new AppUser() { UserName = "admin", Email = "admin@outlook.com" };
    userManager.CreateAsync(newUser,"Pass1234!").Wait();
    userManager.CreateAsync(new AppUser() { UserName = "user", Email = "user@outlook.com"}, "Pass1234!").Wait();
    userManager.CreateAsync(new AppUser() { UserName = "user2", Email = "user2@outlook.com"}, "Pass1234!").Wait();
    // Eğer kullanıcılar yoksa yukarıdaki gibi kullanıcılar oluşturulur

    var newCategory1  = new Category() { Name = "Fiction", UserId = newUser.Id, ReferenceId =0 };
    var newCategory2  = new Category() { Name = "Science", UserId = newUser.Id, ReferenceId =0 };
    var newCategory3  = new Category() { Name = "History", UserId = newUser.Id, ReferenceId =0 };

    appIdentityDbContext.Categories.AddRange(newCategory1, newCategory2, newCategory3);

    appIdentityDbContext.SaveChanges();

    var subCategory1 = new Category() { Name = "Fantasy", UserId = newUser.Id, ReferenceId = newCategory1.Id };

    var subCategory2 = new Category() { Name = "Science Fiction", UserId = newUser.Id, ReferenceId = newCategory2.Id };

    var subCategory3 = new Category() { Name = "Physics", UserId = newUser.Id, ReferenceId = newCategory3.Id };


    appIdentityDbContext.Categories.AddRange(subCategory1, subCategory2, subCategory3);

    appIdentityDbContext.SaveChanges();

    var subChildCategory1 = new Category() { Name = "Epic", UserId = newUser.Id, ReferenceId = subCategory1.Id }; // subCategory1 altında bir alt kategori oluşturuldu

    appIdentityDbContext.Categories.Add(subChildCategory1);

    appIdentityDbContext.SaveChanges();
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
