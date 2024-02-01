using Common.Shared.Events;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Order.API.OrderServices;

namespace Order.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly OrderService _orderService;
        private readonly IPublishEndpoint _publishEndpoint;
        public OrderController(OrderService orderService, IPublishEndpoint publishEndpoint)
        {
            _orderService = orderService;
            _publishEndpoint = publishEndpoint;
        }

        [HttpPost]
        public async Task<IActionResult> Create(OrderCreateRequestDto orderCreateRequestDto)
        {
            var result = await _orderService.CreateAsync(orderCreateRequestDto);

            /*#region Third-party
            var httpClient = new HttpClient();

            var response = await httpClient.GetAsync("https://jsonplaceholder.typicode.com/todos/1"); // fake http request

            var responseContent = await response.Content.ReadAsStringAsync(); 
            #endregion
            */

            return new ObjectResult(result)
            {
                StatusCode = result.StatusCode
            };
        }

        [HttpGet]
        public async Task<IActionResult> SendOrderCreatedEvent()
        {
            // Kuyruğa mesaj gönder
            await _publishEndpoint.Publish<OrderCreatedEvent>(new
            {
                OrderCode = Guid.NewGuid().ToString()
            });

            return Ok();
        }
    }
}
