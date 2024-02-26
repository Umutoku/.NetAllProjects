using Azure;
using Azure.Storage;
using AzureStorageLibrary;
using AzureStorageLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using System.IO.IsolatedStorage;
using System.Net;

namespace MvcWebApp.Controllers
{
    public class TableStoragesController : Controller
    {
        private readonly INoSqlStorage<Product> _productRepository;

        public TableStoragesController(INoSqlStorage<Product> productRepository)
        {
            _productRepository = productRepository;
        }

        public IActionResult Index()
        {
            ViewBag.Products = _productRepository.GetAll().ToList();
            ViewBag.IsUpdate = false;

            return View();
        }
        

        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            product.RowKey = Guid.NewGuid().ToString();
            product.PartitionKey = "product";
            await _productRepository.Add(product);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> UpdateForm(Product product)
        { 
            var products = await _productRepository.Get(product);
            ViewBag.Products = _productRepository.GetAll().ToList();
            ViewBag.IsUpdate = true;

            return View("Index", products);

        }

        [HttpPost]
        public async Task<IActionResult> Update(Product product)
        {
            //try
            //{
            //    await _productRepository.Update(product);
            //}
            //catch (IsolatedStorageException ex)
            //{
            //    if (ex.HResult == (int)HttpStatusCode.PreconditionFailed)
            //    { 
            //        // burada eğer veriyi başkası değiştirmişse uyarı verilebilir.
            //    }
            //    Console.WriteLine(ex.Message);
            //}

            //product.ETag = ETag.All; // ETag.All ile her durumda güncelleme yapılır.

            ViewBag.IsUpdate = true;
            await _productRepository.Update(product);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(Product product)
        {
            await _productRepository.Delete(product);

            return RedirectToAction("Index");
        }

        public IActionResult Query(int price)
        {
            var products = _productRepository.Query(p => p.Price == price).ToList();
            ViewBag.Products = products;
            ViewBag.IsUpdate = false;

            return View("Index");
        }
    }
}
