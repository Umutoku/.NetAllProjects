using Microsoft.AspNetCore.Mvc;
using RedisExchangeAPI.Web.Services;
using StackExchange.Redis;

namespace RedisExchangeAPI.Web.Controllers
{
    public class ListTypeController : Controller
    {
        private readonly RedisService _redisService;
        private readonly IDatabase db;
        private string _next = "names";

        public ListTypeController(RedisService redisService)
        {
            _redisService = redisService;
            db = _redisService.GetDb(1);
        }
        public IActionResult Index()
        {
            List<string> namesList = new List<string>();
            if(db.KeyExists(_next))
            {
                db.ListRange(_next).ToList().ForEach(x =>
                {
                    namesList.Add(x.ToString());
                });
            }

            return View(namesList);
        }
        [HttpPost]
        public IActionResult Add(string name)
        {

            db.ListRightPush(_next, name);

            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult DeleteItem(string name)
        {
            db.ListRemoveAsync(_next, name).Wait();
            return RedirectToAction("Index");
        }
        public IActionResult DeleteFirstItem()
        {
            db.ListLeftPop(_next);
            return RedirectToAction("Index");
        }
    }
}
