using Microsoft.AspNetCore.SignalR.Client;

namespace SignalRWorkerServiceApp
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private HubConnection? hubConnection;
        private readonly IConfiguration _configuration;
        public Worker(ILogger<Worker> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            hubConnection = new HubConnectionBuilder()
                .WithUrl(_configuration.GetConnectionString("Hub")!)
                .WithAutomaticReconnect()
                .Build();

            hubConnection?.StartAsync().ContinueWith(result =>
            {
                if (result.IsFaulted)
                {
                    _logger.LogError("Connection failed");
                    return;
                }
                _logger.LogInformation("Connection started");
            });
            return base.StartAsync(cancellationToken);
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            await base.StopAsync(cancellationToken);
            await hubConnection!.DisposeAsync();
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            hubConnection!.On<Product>("ReceiveTypedMessageForAllClient", (product) =>
            {
                _logger.LogInformation($"ReceiveMessageForAllClient: {product.Id}, {product.Name}");
            });
            return Task.CompletedTask;
        }
    }
}
