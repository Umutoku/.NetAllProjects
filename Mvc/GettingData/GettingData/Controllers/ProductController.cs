using GettingData.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace GettingData.Controllers
{
    public class ProductController : Controller
    {
        List<Product> products= new List<Product>()
        {
            new Product(){ID=1,ProductName="kitap",CategoryName="Ev eşyası"},
            new Product(){ID=2,ProductName="kalem",CategoryName="Ev eşyası"},
            new Product(){ID=3,ProductName="telefon",CategoryName="Elektronik"},
            new Product(){ID=4,ProductName="dolap",CategoryName="Ev eşyası"},
        };
        public IActionResult GetAllProducts()
        {
            return View(products);
        }

        public IActionResult GetProductById(int id)
        {

            Product product = products.Find(x => x.ID == id);
            if(product == null)
            {
                ViewBag.Error="Verilen id ile bir ürün bulunamadı";
            }
            return View(product);
        }
        public IActionResult GetProductByCategoryName(string categoryname)
        {

            List < Product > p = products.FindAll(x => x.CategoryName == categoryname);
            if (p.Count == 0)
            {
                ViewData["Error"] = "Verilen isim ile bir kategori bulunamadı";
            }
            return View(p);
        }
        [HttpPost]
        public IActionResult AddProduct(Product product)
        {
            products.Add(product);

            return View("GetAllProducts", products);
        }
    }
}
