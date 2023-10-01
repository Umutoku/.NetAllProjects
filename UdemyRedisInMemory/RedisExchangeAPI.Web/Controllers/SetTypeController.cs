using Microsoft.AspNetCore.Mvc;
using RedisExchangeAPI.Web.Services;
using StackExchange.Redis;

namespace RedisExchangeAPI.Web.Controllers
{
    public class SetTypeController : Controller
    {
        private readonly RedisService _redisService;
        private readonly IDatabase db;
        private string _next = "hashnames";

        public SetTypeController(RedisService redisService)
        {
            _redisService = redisService;
            db = _redisService.GetDb(2);
        }
        public IActionResult Index()
        {
            HashSet<string> namesHash = new HashSet<string>();
            if(db.KeyExists(_next))
            {
                db.SetMembers(_next).ToList().ForEach(x =>
                {
                    namesHash.Add(x.ToString());
                });
            }

            return View();
        }
        [HttpPost]
        public IActionResult Add(string name)
        {
            //if(!db.KeyExists(_next)) sliding olmaması için
            db.KeyExpire(_next,DateTime.Now.AddMinutes(5));
            db.SetAdd(_next, name);
            //db.SetPop(_next);//random veri siliyor
            //db.SetRandomMember(_next); rastgele veri
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(string name)
        {
            await db.SetRemoveAsync(_next, name);
            return RedirectToAction("Index");
        }
    }
}
