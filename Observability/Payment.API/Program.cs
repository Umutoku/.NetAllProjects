using Common.Shared;
using Logging.Shared;
using OpenTelemetry.Shared;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddOpenTelemetryExt(builder.Configuration);

//builder.Host.UseSerilog(Logging.Shared.Logging.ConfigureLogging); // Serilog için gerekli olan ayarları yapıyoruz

builder.AddOpenTelemetryLog(); // OpenTelemetry için gerekli olan ayarları yapıyoruz
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseMiddleware<OpenTelemetryTraceIdMiddleware>(); // OpenTelemetry ile gelen trace id'yi loglara ekler
app.UseMiddleware<RequestAndResponseActivityMiddleware>();
app.ExceptionMiddlewareLog();

app.UseAuthorization();

app.MapControllers();

app.Run();
