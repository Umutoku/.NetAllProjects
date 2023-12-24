using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLayer.Core;
using NLayer.Core.DTOs;
using NLayer.Core.Repositories;

namespace NLayer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryWithDtoController : CustomBaseController
    {
        private readonly IServiceWithDto<Category, CategoryDto> _serviceWithDto;

        public CategoryWithDtoController(IServiceWithDto<Category, CategoryDto> serviceWithDto)
        {
            _serviceWithDto = serviceWithDto;
        }

        public async Task<IActionResult> Get()
        {
            return CreateActionResult(await _serviceWithDto.GetAllAsync());
        }
    }
}
