using AzureStorageLibrary;
using AzureStorageLibrary.Services;
using MvcWebApp.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSignalR();
builder.Services.AddScoped<IBlobStorage, BlobStorage>(); // bu kod ile IBlobStorage interface'ini implemente eden tüm sınıfların birer örneğini oluşturuyoruz.

ConnectionStrings.AzureStorageConnectionString = builder.Configuration.GetConnectionString("AzureStorage");

//ConnectionStrings.AzureStorageConnectionString = builder.Configuration.GetConnectionString("AzureStorageCloud");

builder.Services.AddScoped(typeof(INoSqlStorage<>), typeof(TableStorage<>)); // bu kod ile INoSqlStorage interface'ini implemente eden tüm sınıfların birer örneğini oluşturuyoruz.
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

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<NotificationHub>("/notificationhub");
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
