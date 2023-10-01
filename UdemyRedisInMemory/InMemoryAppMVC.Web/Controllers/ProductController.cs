using InMemoryAppMVC.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace InMemoryAppMVC.Web.Controllers
{
    
    public class ProductController : Controller
    {

        private readonly IMemoryCache _memoryCache;
        public ProductController(IMemoryCache memoryCache)
        {
             _memoryCache = memoryCache;
        }
        public IActionResult Index()
        {
            //Key memoryde var mı diye kontrol, birinci yol
            //if(String.IsNullOrEmpty(_memoryCache.Get<string>("zaman")))
            //{
            //    _memoryCache.Set<string>("zaman", DateTime.Now.ToString());
            //}
            //2.yol

                MemoryCacheEntryOptions options = new MemoryCacheEntryOptions();
                options.AbsoluteExpiration = DateTime.Now.AddSeconds(30);//ne olursa olsun toplam süre
                options.SlidingExpiration = TimeSpan.FromSeconds(10); //erişilmez ise toplam süre
                options.Priority = CacheItemPriority.Normal;

            //Hangi sebeple memoryden düştü
            options.RegisterPostEvictionCallback((key,value,reason,state) =>
            {
                _memoryCache.Set("callback", $"{key}->{value} => sebep:{reason}");
            });
                _memoryCache.Set<string>("zaman", DateTime.Now.ToString(),options);


            Product p = new Product { Id =1, Name="Kalem",Price=200};
            _memoryCache.Set<Product>("product:1", p);

            return View();
        }

        public IActionResult Show()
        {

            //_memoryCache.Remove("zaman"); // silmek için

            //yoksa yaratmak için
            //_memoryCache.GetOrCreate<string>("zaman", entry =>
            //{
            //    return DateTime.Now.ToString();
            //});

            _memoryCache.TryGetValue("zaman", out var zamancache);
            _memoryCache.TryGetValue("callback", out var callback);
            ViewBag.callback= callback;
            ViewBag.zaman = zamancache;
            ViewBag.product = _memoryCache.Get<Product>("product:1");
            //ViewBag.zaman = _memoryCache.Get<string>("zaman");
            return View();
        }
    }
}
