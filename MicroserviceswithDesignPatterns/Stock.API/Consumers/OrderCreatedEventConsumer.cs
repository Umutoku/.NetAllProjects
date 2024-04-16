using MassTransit;
using Microsoft.EntityFrameworkCore;
using Shared;
using Stock.API.Models;

namespace Stock.API.Consumers
{
    public class OrderCreatedEventConsumer : IConsumer<OrderCreatedEvent>
    {
        private readonly AppDbContext _context;
        private readonly ILogger<OrderCreatedEventConsumer> _logger;
        private readonly ISendEndpointProvider _sendEndpointProvider;
        private readonly IPublishEndpoint _publishEndpoint;

        public OrderCreatedEventConsumer(AppDbContext context, ILogger<OrderCreatedEventConsumer> logger, IPublishEndpoint publishEndpoint, ISendEndpointProvider sendEndpointProvider)
        {
            _context = context;
            _logger = logger;
            _publishEndpoint = publishEndpoint;
            _sendEndpointProvider = sendEndpointProvider;
        }

        public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
        {
            var stockResult = new List<bool>();
            foreach (var item in context.Message.OrderItemMessages)
            {
                stockResult.Add(await _context.Stocks.AnyAsync(x => x.ProductId == item.ProductId && x.Count > item.Count));
            }
            if (stockResult.All(x => x.Equals(true)))
            { 
                foreach (var item in context.Message.OrderItemMessages) // stoktan düşme işlemi
                {
                    var stock = await _context.Stocks.FirstOrDefaultAsync(x => x.ProductId == item.ProductId);
                    stock.Count -= item.Count;
                    _context.Stocks.Update(stock);
                    await _context.SaveChangesAsync();
                    _logger.LogInformation($"Stock {stock.ProductId} updated. Remaining stock: {stock.Count}");

                    var sendEndpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri($"queue:{RabbitMQSettings.StockOrderCreatedEventExchangeName}")); // stok güncellendiğinde order servisine haber veriyoruz

                    StockReservedEvent stockReserveEvent = new StockReservedEvent
                    {
                        Payment = context.Message.Payment,
                        OrderId = context.Message.OrderId,
                        BuyerId = context.Message.BuyerId,
                        OrderItemMessages = context.Message.OrderItemMessages
                    };

                    await sendEndpoint.Send(stockReserveEvent);
                }

            }
            else
            {
                await _publishEndpoint.Publish(new StockNotReservedEvent
                {
                    OrderId = context.Message.OrderId,
                    BuyerId = context.Message.BuyerId,
                    Message = "Stock not reserved"
                });
                _logger.LogInformation("Stock not reserved");
            }
        }
    }
}
