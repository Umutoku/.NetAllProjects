using IDistributedCacheRedisApp.Web.Models;
using IDistributedCacheRedisApp.Web.Services;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using StackExchange.Redis;
using System.Text;
using System.Text.Json;

namespace IDistributedCacheRedisApp.Web.Controllers
{
    public class ProductsController : Controller
    {
        IDistributedCache _distributedCache;
        //private readonly IConnectionMultiplexer _redisCon;
        //private readonly IDatabase _cache;
        //private TimeSpan ExpireTime => TimeSpan.FromDays(1);

        public ProductsController( IDistributedCache distributedCache)
        {
            //_redisCon = redisCon;
            //_cache = redisCon.GetDatabase();
            _distributedCache = distributedCache;
        }
        public async Task<IActionResult> Index()
        {
            DistributedCacheEntryOptions distributedCacheEntryOptions = new DistributedCacheEntryOptions();
            distributedCacheEntryOptions.AbsoluteExpiration =DateTime.Now.AddMinutes(1);
            //await _distributedCache.SetStringAsync("name", "umut oku",distributedCacheEntryOptions);
            Product product = new Product { Id=1,Name="kalem",Price=100};
        string jsonproduct= JsonConvert.SerializeObject(product);
            Byte[] byteproduct =Encoding.UTF8.GetBytes(jsonproduct);
           await  _distributedCache.SetAsync("product:1", byteproduct,distributedCacheEntryOptions);

            return View();
        }

        public IActionResult Show()
        {

            //string jsonproduct = _distributedCache.GetString("product:1");
            Byte[] byteproduct = _distributedCache.Get("product:1");
            string jsonproduct = Encoding.UTF8.GetString(byteproduct);

            Product p = JsonConvert.DeserializeObject<Product>(jsonproduct);
            ViewBag.product = p;
            return View();
        }

        public IActionResult Delete()
        {
            _distributedCache.Remove("name");
            return View();
        }

        public IActionResult ImageCache()
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(),
                "wwwroot/images/download.jpg");
            Byte[] imageByte = System.IO.File.ReadAllBytes(path);
            _distributedCache.Set("resim",imageByte);
            return View();
        }

        public IActionResult ImageUrl()
        {
            byte[] resimbyte = _distributedCache.Get("resim");
            return File(resimbyte, "image/jpg");
        }
    }
}
