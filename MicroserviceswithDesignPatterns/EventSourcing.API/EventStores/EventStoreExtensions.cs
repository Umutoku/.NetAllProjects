using EventStore.Client;

namespace EventSourcing.API.EventStores
{
    public static class EventStoreExtensions
    {
        public static void AddEventStore(this IServiceCollection services, IConfiguration configuration)
        {
            var settings = EventStoreClientSettings.Create(configuration["EventStore:ConnectionString"]);
            var eventStoreClient = new EventStoreClient(settings);
            
            services.AddSingleton(eventStoreClient);

            using var logFactory = LoggerFactory.Create(builder =>
            {
                builder.SetMinimumLevel(LogLevel.Information);
                builder.AddConsole();
            });

            var logger = logFactory.CreateLogger<EventStoreClient>();


        }
    }
}
