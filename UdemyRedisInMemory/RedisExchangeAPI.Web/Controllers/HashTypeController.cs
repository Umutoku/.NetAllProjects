using Microsoft.AspNetCore.Mvc;
using RedisExchangeAPI.Web.Services;
using StackExchange.Redis;

namespace RedisExchangeAPI.Web.Controllers
{
    public class HashTypeController : Controller
    {
        private readonly RedisService _redisService;
        private readonly IDatabase db;
        public string hashKey { get; set; } = "sözlük";

        public HashTypeController(RedisService redisService)
        {
            _redisService = redisService;
            db = _redisService.GetDb(4);
        }
        public IActionResult Index()
        {
            Dictionary<string,string> dic = new Dictionary<string,string>();
            if(db.KeyExists(hashKey))
            {
                db.HashGetAll(hashKey).ToList().ForEach(x =>
                {
                    dic.Add(x.Name, x.Value);
                });
            }
            return View(dic);
        }
        public IActionResult Add(string name,string value)
        {
            db.HashSet(hashKey,name, value);
            return RedirectToAction("Index");
        }
        public IActionResult Delete(string name)
        {
            db.HashDelete(hashKey,name);
            return RedirectToAction("Index");
        }
    }
}
