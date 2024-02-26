var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options => // sayesinde CORS politikalarını ekleyebiliriz
{
    options.AddPolicy("AllowAll", 
               builder => builder.AllowAnyOrigin()
                          .AllowAnyMethod()
                                     .AllowAnyHeader());

    options.AddPolicy("AllowSpecific", 
                      builder => builder.WithOrigins("http://localhost:5000")
                                               .WithMethods("GET", "POST", "PUT", "DELETE")
                                                                                   .WithHeaders("accept", "content-type", "origin", "x-custom-header"));

    options.AddPolicy("AllowSubdomain", 
                             builder => builder.WithOrigins("http://*.contoso.com").SetIsOriginAllowedToAllowWildcardSubdomains()); // sayesinde subdomainlere izin veririz

    options.AddPolicy("AllowLocalhost",builder => builder.WithOrigins("http://localhost:5000").AllowCredentials()); // sayesinde localhosta izin veririz
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseHttpsRedirection();

app.UseCors("AllowAll"); // CORS politikalarını ekledik

app.UseAuthorization();

app.MapControllers();

app.Run();
