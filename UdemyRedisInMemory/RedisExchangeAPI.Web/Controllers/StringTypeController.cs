using Microsoft.AspNetCore.Mvc;
using RedisExchangeAPI.Web.Services;
using StackExchange.Redis;

namespace RedisExchangeAPI.Web.Controllers
{
    public class StringTypeController : Controller
    {
        private readonly RedisService _redisService;
        private readonly IDatabase db;
        public StringTypeController(RedisService redisService)
        {
            _redisService = redisService;
            db = _redisService.GetDb(0);
        }
        public IActionResult Index()
        {
            
            db.StringSet("name", "umut okuuuu");
            db.StringSet("deneme", 1000);
            return View();
        }

        public IActionResult Show() 
        {
            var value = db.StringGet("name");
            var valuecut = db.StringGetRange("name", 0, 3);
            var valuelent = db.StringLength("name");
            //db.StringIncrement("deneme", 1);
            var _ = db.StringDecrementAsync("deneme").Result;

            db.StringDecrementAsync("deneme",10).Wait();
            if(value.HasValue)
            {
                ViewBag.value = value.ToString();
            }
            return View();
        }
    }
}
