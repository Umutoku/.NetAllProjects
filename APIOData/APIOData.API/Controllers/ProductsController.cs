using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Attributes;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using OData.API.Models;
using UdemyAPIOData.API.Models;

namespace UdemyAPIOData.API.Controllers
{
    [EnableQuery] // EnableQuery attribute is used to enable OData query options // EnableQuery özniteliği OData sorgu seçeneklerini etkinleştirmek için kullanılır
    public class ProductsController : ODataController
    {
        private readonly AppDbContext _context;

        public ProductsController(AppDbContext context)
        {
            _context = context;
        }
        // 
        [HttpGet]
        [EnableQuery(PageSize = 5,AllowedArithmeticOperators = AllowedArithmeticOperators.All)] // EnableQuery attribute is used to enable OData query options // EnableQuery özniteliği OData sorgu seçeneklerini etkinleştirmek için kullanılır
        public IQueryable<Product> Get()
        {
            return _context.Products;
        }

        [HttpGet("{id:int}")]
        public Product Get(int id)
        {
            return _context.Products.Find(id);
        }

        [HttpPost]
        public Product Post([FromBody] Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
            return product;
        }

        [HttpPut]
        public Product Put([FromODataUri]int key, [FromBody] Product product)
        {
            product.Id = key;
            _context.Products.Update(product);
            _context.SaveChanges();
            return product;
        }

        [HttpDelete]
        public IActionResult Delete([FromODataUri] int key)
        {
            var product = _context.Products.Find(key);
            if (product == null)
            {
                return NotFound();
            }
            _context.Products.Remove(product);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpPost]
        public string Login(ODataActionParameters parameters)
        {
            Login login = parameters["login"] as Login;

            return $"{login.Email} - {login.Password}";
        }
        //products/multiplyFunc(number1=5,number2=10)
        [HttpGet]
        public IActionResult MultiplyFunc([FromODataUri] int number1, [FromODataUri] int number2)
        {
            return Ok(number1 * number2);
        }

        [HttpGet]
        public IActionResult Tax(int key,[FromODataUri] double kdv)
        {
            var product = _context.Products.Find(key);
            if (product == null)
            {
                return NotFound();
            }
            return Ok((double)product.Price * kdv);
        }

    }
}