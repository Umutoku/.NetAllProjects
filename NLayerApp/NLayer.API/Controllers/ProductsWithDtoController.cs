using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLayer.API.Filters;
using NLayer.Core;
using NLayer.Core.DTOs;
using NLayer.Core.Services;

namespace NLayer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsWithDtoController : CustomBaseController
    {
        private readonly IProductServiceWithDto _productServiceWithDto;

        public ProductsWithDtoController(IProductServiceWithDto productServiceWithDto)
        {
            _productServiceWithDto = productServiceWithDto;
        }
        
           [HttpGet("GetProductsWithCategory")]
        public async Task<IActionResult> GetProductsWithCategory()
        {
            return CreateActionResult(await _productServiceWithDto.GetProductWithCategory());
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            return CreateActionResult(await _productServiceWithDto.GetAllAsync());
        }
        
        [ServiceFilter(typeof(NotFoundFilter<Product>))]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            return CreateActionResult(await _productServiceWithDto.GetByIdAsync(id));
        }
        
        [HttpPost]
        public async Task<IActionResult> Save(ProductDto productDto)
        {
            var product = await _productServiceWithDto.AddAsync(productDto);
            return CreateActionResult(product);
        }
        
        [HttpPut]
        public async Task<IActionResult> Update(ProductDto productDto)
        {
            return CreateActionResult(await _productServiceWithDto.UpdateAsync(productDto));
        }
        
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Remove(int id)
        {
            var remove = await _productServiceWithDto.RemoveAsync(id);
            return CreateActionResult(remove);
        }
        
        [HttpPost("SaveAll")]
        public async Task<IActionResult> Save(List<ProductDto> products)
        {
            return CreateActionResult(await _productServiceWithDto.AddRangeAsync(products));
        }
        
        [HttpDelete("RemoveAll")]
        public async Task<IActionResult> RemoveAll(List<int> ids)
        {
            return CreateActionResult(await _productServiceWithDto.RemoveRangeAsync(ids));
        }
        
        
        
        
    }
}
