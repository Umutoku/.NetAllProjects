using MassTransit;
using Shared;

namespace Payment.API.Consumers
{
    public class StockReservedEventConsumer : IConsumer<StockReservedEvent>
    {
        private readonly ILogger<StockReservedEventConsumer> _logger;
        private readonly IPublishEndpoint _publishEndpoint;

        public StockReservedEventConsumer(IPublishEndpoint publishEndpoint, ILogger<StockReservedEventConsumer> logger)
        {
            _publishEndpoint = publishEndpoint;
            _logger = logger;
        }

        public Task Consume(ConsumeContext<StockReservedEvent> context)
        {
            var balance = 3000m;
            if(balance> context.Message.Payment.Total)
            {
                _logger.LogInformation($"StockReservedEventConsumer: Stock reserved for order id {context.Message.OrderId}");

                var paymentSuccessedEvent = new PaymentCompletedEvent
                {
                    OrderId = context.Message.OrderId,
                    BuyerId = context.Message.BuyerId,

                };
                _publishEndpoint.Publish(paymentSuccessedEvent);
                return Task.CompletedTask;
            }
            else
            {
                _logger.LogInformation($"StockReservedEventConsumer: Stock could not be reserved for order id {context.Message.OrderId}");
                var paymentFailedEvent = new PaymentFailedEvent
                {
                    OrderId = context.Message.OrderId,
                    BuyerId = context.Message.BuyerId,
                    OrderItemMessages = context.Message.OrderItemMessages,
                    Message = "Insufficient balance"
                };
                _publishEndpoint.Publish(paymentFailedEvent);
                return Task.CompletedTask;
            }
        }
    }
}
