using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews(opts =>
{
    opts.Filters.Add(new AutoValidateAntiforgeryTokenAttribute()); // bu attribute ile formun token kontrolü yapılır
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

app.UseHsts(); // bu kod ile http üzerinden gelen istekler https'e yönlendirilir usehttpsredirection'dan önce olmalıdır ve farkı ise usehttpsredirection sadece http isteklerini https'e yönlendirirken usehsts tüm istekleri yönlendirir

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
