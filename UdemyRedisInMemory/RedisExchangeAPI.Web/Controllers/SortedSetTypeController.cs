using Microsoft.AspNetCore.Mvc;
using RedisExchangeAPI.Web.Services;
using StackExchange.Redis;

namespace RedisExchangeAPI.Web.Controllers
{
    public class SortedSetTypeController : Controller
    {
        private readonly RedisService _redisService;
        private readonly IDatabase db;
        private string _listKey = "sortedsetnames";

        public SortedSetTypeController(RedisService redisService)
        {
            _redisService = redisService;
            db = _redisService.GetDb(3);
        }
        public IActionResult Index()
        {
            HashSet<string> list = new HashSet<string>();
            if(db.KeyExists(_listKey))
            {
                //db.SortedSetScan(_listKey).ToList().ForEach(x =>
                //{
                //    list.Add(x.ToString());
                //});
                db.SortedSetRangeByRank(_listKey, order: Order.Ascending).ToList().ForEach(x =>
                {
                    list.Add(x.ToString());
                });
            }

            return View(list);
        }
        [HttpPost]
        public IActionResult Add(string name, int score)
        {
            db.SortedSetAdd(_listKey, name, score);
            db.KeyExpire(_listKey,DateTime.Now.AddMinutes(1));

            return RedirectToAction("Index");
        }

        public IActionResult DeleteItem(string name)
        {
            db.SortedSetRemove(_listKey, name);
            return RedirectToAction("index");
        }
    }
}
