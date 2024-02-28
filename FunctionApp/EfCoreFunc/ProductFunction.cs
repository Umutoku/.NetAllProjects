using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using EfCoreFunc.Models;

namespace EfCoreFunc
{
    public class ProductFunction
    {
        private readonly AppDbContext _context;
        private const string Route = "product";
        public ProductFunction(AppDbContext context)
        {
            _context = context;
        }
        

        [FunctionName("GetProducts")]
        public async Task<IActionResult> GetProducts(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = Route)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Tüm ürünleri getir");

            var products = await _context.Products.ToListAsync();   

            return new OkObjectResult(products);
            
        }

        [FunctionName("GetProductById")]
        public async Task<IActionResult> GetProductById(
                       [HttpTrigger(AuthorizationLevel.Function, "get", Route = Route + "/{id}")] HttpRequest req,
                                  ILogger log, string id)
        {
            log.LogInformation("Ürün getir");

            var product = await _context.Products.FindAsync(int.Parse(id));

            if (product == null)
            {
                return new NotFoundResult();
            }

            return new OkObjectResult(product);
        }

        [FunctionName("CreateProduct")]
        public async Task<IActionResult> CreateProduct(
                       [HttpTrigger(AuthorizationLevel.Function, "post", Route = Route)] HttpRequest req,
                                  ILogger log)
        {
            log.LogInformation("Ürün ekle");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync(); 
            var input = JsonConvert.DeserializeObject<Product>(requestBody);

            await _context.Products.AddAsync(input);
            await _context.SaveChangesAsync();

            return new OkObjectResult(input);
        }

        [FunctionName("UpdateProduct")]
        public async Task<IActionResult> UpdateProduct(
                                  [HttpTrigger(AuthorizationLevel.Function, "put", Route = Route + "/{id}")] HttpRequest req,
                                                                   ILogger log, string id)
        {
            log.LogInformation("Ürün güncelle");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var updated = JsonConvert.DeserializeObject<Product>(requestBody);

            var product = await _context.Products.FindAsync(int.Parse(id));

            if (product == null)
            {
                return new NotFoundResult();
            }

            _context.Update(updated);

            await _context.SaveChangesAsync();

            return new OkObjectResult(product);
        }

        [FunctionName("DeleteProduct")]
        public async Task<IActionResult> DeleteProduct(
                                             [HttpTrigger(AuthorizationLevel.Function, "delete", Route = Route + "/{id}")] HttpRequest req,
                                                                                                               ILogger log, string id)
        {
            log.LogInformation("Ürün sil");

            var product = await _context.Products.FindAsync(int.Parse(id));

            if (product == null)
            {
                return new NotFoundResult();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return new OkResult();
        }
    }
}
