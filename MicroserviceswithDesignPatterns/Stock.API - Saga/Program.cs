using MassTransit;
using Microsoft.EntityFrameworkCore;
using SharedSaga;
using Stock.API.Models;
using Stock.APISaga.Consumers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(opt=>opt.UseInMemoryDatabase("StockDb"));

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<StockRollbackMessageConsumer>();
    x.AddConsumer<OrderCreatedEventConsumer>();
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(builder.Configuration.GetConnectionString("RabbitMQ"));

        cfg.ReceiveEndpoint(RabbitMQSettings.StockOrderCreatedEventQueueName, e =>
        {
            e.ConfigureConsumer<OrderCreatedEventConsumer>(context);
        });

        cfg.ReceiveEndpoint(RabbitMQSettings.StockRollbackMessageQueueName, e =>
        {
            e.ConfigureConsumer<StockRollbackMessageConsumer>(context);
        });
    });
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider; // sayesinnde servislerimize erişebiliriz
    var context = services.GetRequiredService<AppDbContext>(); // contextimizi alıyoruz

    context.Add(new Stock.API.Models.Stock { ProductId = 1, Count = 100 }); // örnek bir veri ekliyoruz
    context.Add(new Stock.API.Models.Stock { ProductId = 2, Count = 200 }); // örnek bir veri ekliyoruz
    context.Add(new Stock.API.Models.Stock { ProductId = 3, Count = 300 }); // örnek bir veri ekliyoruz
    context.SaveChanges(); // değişiklikleri kaydediyoruz

    context.Database.EnsureCreated(); // eğer db yoksa oluşturuyoruz
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
