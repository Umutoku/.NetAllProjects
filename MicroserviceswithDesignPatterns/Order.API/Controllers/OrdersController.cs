using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Order.API.DTOs;
using Order.API.Models;
using Shared;

namespace Order.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IPublishEndpoint _publishEndpoint;

        public OrdersController(AppDbContext context, IPublishEndpoint publishEndpoint)
        {
            _context = context;
            _publishEndpoint = publishEndpoint;
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

            var OrderCreatedEvent = new OrderCreatedEvent()
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
                    Total = orderCreate.orderItems.Sum(x => x.Price * x.Count)
                    
                },

            };
            orderCreate.orderItems.ForEach(item =>
            {
                OrderCreatedEvent.OrderItemMessages.Add(new OrderItemMessage
                {
                    ProductId = item.ProductId,
                    Count = item.Count,
                });
            });

            await _publishEndpoint.Publish<OrderCreatedEvent>(OrderCreatedEvent);


            return Ok();
        }
    }
}
