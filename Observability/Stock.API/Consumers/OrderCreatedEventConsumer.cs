using Common.Shared.Events;
using MassTransit;
using System.Diagnostics;
using System.Text.Json;

namespace Stock.API.Consumers
{
    public class OrderCreatedEventConsumer : IConsumer<OrderCreatedEvent>
    {
        public Task Consume(ConsumeContext<OrderCreatedEvent> context)
        {
            Thread.Sleep(1000);

            Activity.Current?.SetTag("message.body",JsonSerializer.Serialize(context.Message)); // Bu sayede taglarda mesaj içeriğini görebiliriz.

            return Task.CompletedTask;
        }
    }
}
