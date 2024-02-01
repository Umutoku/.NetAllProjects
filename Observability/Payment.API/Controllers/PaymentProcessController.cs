using Common.Shared.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

namespace Payment.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PaymentProcessController : ControllerBase
    {
        private readonly ILogger<PaymentProcessController> _logger;

        public PaymentProcessController(ILogger<PaymentProcessController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public IActionResult Create(PaymentCreateRequestDto paymentCreateRequestDto)
        {
            // traceparent sayesinde request'in hangi requestten geldiğini anlayabiliriz
            if(HttpContext.Request.Headers.TryGetValue("traceparent",out StringValues values)) // traceparent header'ını okumak için
                Console.WriteLine($"traceParent: {values.First()}"); 

            const decimal balance = 1000;

            if(balance < paymentCreateRequestDto.TotalPrice)
            {
                _logger.LogError("Balance is not enough {@paymentCreateRequestDto}", paymentCreateRequestDto);
                return new ObjectResult(new ResponseDto<PaymentCreateResponseDto>
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Errors = new List<string> { "Balance is not enough" }
                });
            }
            _logger.LogInformation("Payment is created {@paymentCreateRequestDto}", paymentCreateRequestDto);
            return new ObjectResult(new ResponseDto<PaymentCreateResponseDto>
            {
                StatusCode = StatusCodes.Status200OK,
                Data = new PaymentCreateResponseDto
                {
                    Description = "Payment is created"
                }
            });
        }
    }
}
