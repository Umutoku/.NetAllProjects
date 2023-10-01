using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RateLimitAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        [HttpGet]
        public ActionResult GetProduct()
        {
            return Ok(new { id = 1, Name = "kalem", Price = 20 });
        }

        [HttpGet("{name}/{price}")]
        public ActionResult GetProduct(string name,int price) {
            return Ok(name);

        }
    }
}
