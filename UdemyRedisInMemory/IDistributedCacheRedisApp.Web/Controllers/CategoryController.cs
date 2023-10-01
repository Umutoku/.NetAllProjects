using IDistributedCacheRedisApp.Web.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IDistributedCacheRedisApp.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpGet("category/getall")]
        public IActionResult GetAll()
        {
            return Ok(_categoryService.GetAllCategory());
        }
    }
}
