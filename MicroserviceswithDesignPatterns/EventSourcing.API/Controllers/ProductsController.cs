using EventSourcing.API.Commands;
using EventSourcing.API.DTOs;
using EventSourcing.API.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventSourcing.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateProductDto createProductDto)
        {
            var command = new CreateProductCommand { CreateProductDto = createProductDto };
            await _mediator.Send(command);
            return Ok();
        }

        [HttpPut("{id}/name")]
        public async Task<IActionResult> PutName(Guid id, [FromBody] ChangeProductNameDto changeProductNameDto)
        {
            var command = new ChangeProductNameCommand { ChangeProductNameDto = changeProductNameDto};
            await _mediator.Send(command);
            return Ok();
        }

        [HttpPut("{id}/price")]
        public async Task<IActionResult> PutPrice(Guid id, [FromBody] ChangeProductPriceDto changeProductPriceDto)
        {
            var command = new ChangeProductPriceCommand { ChangeProductPriceDto = changeProductPriceDto };
            await _mediator.Send(command);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteProductCommand { Id = id };
            await _mediator.Send(command);
            return Ok();
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> Get(int userId)
        {
            var query = new GetAllProductListByUserId { UserId = userId };
            var products = await _mediator.Send(query);
            return Ok(products);
        }
    }
}
