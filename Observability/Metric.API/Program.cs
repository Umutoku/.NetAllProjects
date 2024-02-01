using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddOpenTelemetry().WithMetrics(options => //withmetrics sayesinde metric'leri configure edebiliriz
{
    options.AddMeter("metric.api");
    options.ConfigureResource(resource =>
    {
        resource.AddService("Metric.API", serviceVersion: "1.0.0");
    });
    options.AddPrometheusExporter(); // prometheus exporter'ı ekliyoruz ki prometheus üzerinden metric'leri görebilelim
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
   
}

app.UseSwagger();
app.UseSwaggerUI();
app.UseOpenTelemetryPrometheusScrapingEndpoint(); // prometheus endpoint'ini kullanabilmek için ekliyoruz. meters endpoint'ini kullanabilmek için ekliyoruz


//app.UseHttpsRedirection(); // https redirection'ı kapatıyoruz. Çünkü nginx üzerinden gelen istekler http olarak geliyor. https üzerinden gelen istekler için bu middleware'ı kullanabiliriz.

app.UseAuthorization();

app.MapControllers();

app.Run();
