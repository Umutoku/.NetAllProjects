using Common.Shared;
using Logging.Shared;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using OpenTelemetry.Shared;
using Order.API.Models;
using Order.API.OrderServices;
using Order.API.RedisServices;
using Order.API.StockServices;
using Serilog;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<OrderService>();
builder.Services.AddScoped<StockService>();
builder.Services.AddSingleton<RedisService>(_ =>
{
    return new RedisService(builder.Configuration);
});

builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("localhost","/",host =>
        {
            host.Username("guest");
            host.Password("guest");
        });

    });
});

builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
{
    var redisService = sp.GetRequiredService<RedisService>();
    return redisService.GetConnectionMultiplexer;
});

builder.Services.AddHttpClient<StockService>(opt =>
{
    opt.BaseAddress = new Uri(builder.Configuration.GetSection("ApiServices")["StockApi"]!);
});
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<OrderService>(); // newlemeye gerek yok. Constructor içerisinde parametre olarak alınabilir.
// Dal her zaman scpoed olmalıdır. Çünkü her request için yeni bir dal oluşturulmalıdır. Transaction bütünlüğü için

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"));
});

//builder.Host.UseSerilog(Logging.Shared.Logging.ConfigureLogging); // Serilog için gerekli olan ayarları yapıyoruz
builder.AddOpenTelemetryLog(); 
builder.Services.AddOpenTelemetryExt(builder.Configuration); // OpenTelemetry için gerekli olan ayarları yapıyoruz


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
