
using EventSourcing.API.EventStores;
using EventSourcing.API.Models;
using EventSourcing.Shared.Events;
using EventStore.Client;
using System.Text;
using System.Text.Json;

namespace EventSourcing.API.BackgroundServices
{
    public class ProductReadModelEventStore : BackgroundService
    {
        private readonly EventStoreClient _eventStoreClient;
        private readonly ILogger<ProductReadModelEventStore> _logger;
        //Singletonda scope'a erişim olmadığı için serviceProvider alacağız
        private readonly IServiceProvider _serviceProvider;

        public ProductReadModelEventStore(EventStoreClient eventStoreClient, ILogger<ProductReadModelEventStore> logger, IServiceProvider serviceProvider)
        {
            _eventStoreClient = eventStoreClient;
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var subscription = _eventStoreClient.SubscribeToStreamAsync(
                "your-stream-name",
                FromStream.Start,
                async (subscription, @event, cancellationToken) => await EventAppeared(subscription, @event),
                resolveLinkTos: true,
                cancellationToken: stoppingToken
            );

            return Task.CompletedTask;
        }



        private async Task EventAppeared(StreamSubscription subscription, ResolvedEvent @event)
        {
            var eventType = @event.Event.EventType;
            var eventData = Encoding.UTF8.GetString(@event.Event.Data.Span);

            _logger.LogInformation($"Event received: Type='{eventType}', Data='{eventData}'");

            // Deserialize event data based on event type
            // Example: var productCreatedEvent = JsonSerializer.Deserialize<ProductCreatedEvent>(eventData);

            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            // Handle different event types
            switch (eventType)
            {
                case "ProductCreatedEvent":
                    // Deserialize event data into corresponding model
                    var productCreatedEvent = JsonSerializer.Deserialize<ProductCreatedEvent>(eventData);

                    // Process the event accordingly
                    var product = new Product
                    {
                        Name = productCreatedEvent.Name,
                        Id = productCreatedEvent.Id,
                        Price = productCreatedEvent.Price,
                        Stock = productCreatedEvent.Stock,
                        UserId = productCreatedEvent.UserId
                    };
                    context.Products.Add(product);
                    break;

                case "ProductNameChangedEvent":
                    // Deserialize event data into corresponding model
                    var productNameChangedEvent = JsonSerializer.Deserialize<ProductNameChangedEvent>(eventData);

                    // Process the event accordingly
                    var existingProduct = context.Products.FirstOrDefault(p => p.Id == productNameChangedEvent.Id);
                    if (existingProduct != null)
                    {
                        existingProduct.Name = productNameChangedEvent.ChangedName;
                    }
                    break;

                case "ProductPriceChangedEvent":
                    // Deserialize event data into corresponding model
                    var productPriceChangedEvent = JsonSerializer.Deserialize<ProductPriceChangedEvent>(eventData);

                    // Process the event accordingly
                    var existingProduct = context.Products.FirstOrDefault(p => p.Id == productPriceChangedEvent.Id);
                    if (existingProduct != null)
                    {
                        existingProduct.Price = productPriceChangedEvent.ChangedPrice;
                    }
                    break;

                case "ProductDeletedEvent":
                    // Deserialize event data into corresponding model
                    var productDeletedEvent = JsonSerializer.Deserialize<ProductDeletedEvent>(eventData);

                    // Process the event accordingly
                    var existingProduct = context.Products.FirstOrDefault(p => p.Id == productDeletedEvent.Id);
                    if (existingProduct != null)
                    {
                        context.Products.Remove(existingProduct);
                    }
                    break;
            }

            await context.SaveChangesAsync();

            // Acknowledge the event to mark it as processed
            await subscription.Ack(@event);
        }




    }
}
