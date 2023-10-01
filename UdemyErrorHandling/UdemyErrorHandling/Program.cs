using UdemyErrorHandling.Filter;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews(options =>
{
    options.Filters
    .Add(new CustomHandleExceptionFilterAttribute() { ErrorPage="CustomError1"});// her yerde bu hata uygulanacak
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseStatusCodePages("text/plain","Bir hata var. Durum kodu:{0}"); //2 farklı error
    app.UseStatusCodePages(async context =>
    {
        context.HttpContext.Response.ContentType = "text/html";
        await context.HttpContext.Response
        .WriteAsync($"Bir hata var, durum kodu:{context.HttpContext.Response.StatusCode}");

    });
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
