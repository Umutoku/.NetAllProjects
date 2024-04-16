using MassTransit;
using Microsoft.EntityFrameworkCore;
using SagaStateMachineWorkerService;
using SagaStateMachineWorkerService.Models;
using SharedSaga;
using System.Reflection;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();

builder.Services.AddMassTransit(x =>
{
    x.AddSagaStateMachine<OrderStateMachine, OrderStateInstance>()
        .EntityFrameworkRepository(r =>
        {
            r.ConcurrencyMode = ConcurrencyMode.Pessimistic; // This is the concurrency mode, bu kod ile aynı anda aynı kayıt üzerinde işlem yapılmasını engelliyoruz
            r.AddDbContext<DbContext,OrderStateDbContext>((provider, builder) =>
            {
                builder.UseSqlServer(provider.GetRequiredService<IConfiguration>().GetConnectionString("OrderStateDb"),m=>m.MigrationsAssembly(Assembly.GetExecutingAssembly().GetName().Name)); // This is the connection string of the database where the saga instance will be stored
            });
        });

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("rabbitmq://localhost");
        cfg.ReceiveEndpoint(RabbitMQSettings.OrderSagaQueueName, e =>
        {
            e.ConfigureSaga<OrderStateInstance>(context);
        });
        cfg.ConfigureEndpoints(context);
    });
});

var host = builder.Build();
host.Run();
