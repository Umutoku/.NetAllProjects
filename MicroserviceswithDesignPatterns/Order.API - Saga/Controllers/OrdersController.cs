using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Order.API.DTOs;
using Order.API.Models;
using SharedSaga;
using SharedSaga.Events;
using SharedSaga.Interfaces;



namespace Order.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly ISendEndpointProvider _sendEndpointProvider;

        public OrdersController(AppDbContext context, IPublishEndpoint publishEndpoint, ISendEndpointProvider sendEndpointProvider)
        {
            _context = context;
            _publishEndpoint = publishEndpoint;
            _sendEndpointProvider = sendEndpointProvider;
        }

        [HttpPost]
        public async Task<IActionResult> PostOrder(OrderCreateDto orderCreate)
        {
            var order = new Models.Order
            {
                BuyerId = orderCreate.BuyerId,
                Status = OrderStatus.Suspend,
                CreatedDate = DateTime.Now,
                Address = new Address
                {
                    Line = orderCreate.Address.Line,
                    Province = orderCreate.Address.Province,
                    District = orderCreate.Address.District
                }
                
            };

            orderCreate.orderItems.ForEach(item =>
            {
                order.OrderItems.Add(new OrderItem
                {
                    ProductId = item.ProductId,
                    Count = item.Count,
                    Price = item.Price
                });
            });

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            var orderCreatedRequestEvent = new OrderCreatedRequestEvent()
            {
                OrderId = order.Id, 
                BuyerId = orderCreate.BuyerId,
                Payment = new PaymentMessage
                {
                    CardName = orderCreate.Payment.CardName,
                    CardNumber = orderCreate.Payment.CardNumber,
                    CardType = orderCreate.Payment.CardType,
                    ExpiryMonth = orderCreate.Payment.ExpiryMonth,
                    ExpiryYear = orderCreate.Payment.ExpiryYear,
                    CVV = orderCreate.Payment.CVV,
                    TotalPrice = orderCreate.orderItems.Sum(x => x.Price * x.Count)
                    
                },

            };
            orderCreate.orderItems.ForEach(item =>
            {
                orderCreatedRequestEvent.OrderItems.Add(new OrderItemMessage
                {
                    ProductId = item.ProductId,
                    Count = item.Count,
                });
            });

            //await _publishEndpoint.Publish<OrderCreatedEvent>(OrderCreatedEvent);

            var sendEndpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri($"queue:{RabbitMQSettings.OrderSagaQueueName}"));

            await sendEndpoint.Send<IOrderCreatedRequestEvent>(orderCreatedRequestEvent); // send to saga

            return Ok();
        }
    }
}
