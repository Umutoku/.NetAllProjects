using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Attributes;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using UdemyAPIOData.API.Models;

namespace UdemyAPIOData.API.Controllers
{
    //[ODataRoutePrefix("categories")] // ODataRoutePrefix özniteliği, OData sorguları için kullanılan route adını belirtmek için kullanılır.
    [EnableQuery]
    public class CategoriesController : ODataController
    {
        private readonly AppDbContext _context;

        public CategoriesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IQueryable<Category> Get()
        {
            return _context.Categories;
        }

        [HttpGet]
        public IQueryable<Product> GetProducts([FromODataUri] int key)
        {
            return _context.Products.Where(x => x.Id == key);
        }
        [ODataRouteComponent("item")] // ODataRouteComponent özniteliği, OData sorguları için kullanılan route bileşenlerini belirtmek için kullanılır.
        public IActionResult GetUrunler([FromODataUri] int item)
        {
            var category = _context.Categories.Find(item);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category.Products);
        }

        [ODataRouteComponent("Categories({id})/products({item}")] // ODataRouteComponent özniteliği, OData sorguları için kullanılan route bileşenlerini belirtmek için kullanılır.
        public IActionResult GetProductFromCategories([FromODataUri] int id, [FromODataUri] int item)
        {
            var category = _context.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }
            var product = category.Products.FirstOrDefault(x => x.Id == item);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPost] //Eğer action ise HttpPost özniteliği kullanılır.
        public IActionResult TotalPrice([FromODataUri] int key)
        {
            var category = _context.Categories.Find(key);
            if (category == null)
            {
                return NotFound();
            }
            var totalPrice = category.Products.Sum(x => x.Price);
            return Ok(totalPrice);
        }

        [HttpPost] //Eğer action ise HttpPost özniteliği kullanılır.
        public IActionResult TotalProductPrice(ODataActionParameters parameters) // ODataActionParameters, OData action'ları için kullanılan parametreleri temsil eder.
        {
            int categoryId = (int)parameters["categoryId"];
            var category = _context.Categories.Find(categoryId);
            if (category == null)
            {
                return NotFound();
            }
            var totalPrice = category.Products.Sum(x => x.Price);
            return Ok(totalPrice);
        }

        [HttpPost]
        public IActionResult GetProducts(ODataActionParameters parameters)
        {
            int categoryId = (int)parameters["categoryId"];
            var category = _context.Categories.Find(categoryId);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category.Products);
        }

        [HttpGet]
        public IActionResult CategoryCount()
        {
            return Ok(_context.Categories.Count());
        }


    }
}
