using MassTransit;
using Microsoft.EntityFrameworkCore;
using Order.API.Models;
using Shared;

namespace Order.API.Consumers
{
    public class PaymentFailedEventConsumer : IConsumer<PaymentFailedEvent>
    {
        private readonly AppDbContext _context;
        private readonly ILogger<PaymentFailedEventConsumer> _logger;

        public PaymentFailedEventConsumer(AppDbContext context, ILogger<PaymentFailedEventConsumer> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<PaymentFailedEvent> context)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(x => x.Id == context.Message.OrderId);

            if (order != null)
            {
                order.Status = OrderStatus.Failed;
                order.FailMessage = context.Message.Message;
                _context.Orders.Update(order);
                _context.SaveChanges();
                _logger.LogInformation($"Order {order.Id} failed.");
            }
            else
            {
                _logger.LogError($"Order {context.Message.OrderId} not found.");
            }
        }
    }
}
