using IocContainertwo.Services;
using Microsoft.AspNetCore.Mvc;

namespace IocContainertwo.Controllers
{
    public class HomeController : Controller
    {
        public string Index()
        {
            ConsoleLogger log = new ConsoleLogger();
            log.Info("Yeni bir istek gelmiştir.");
            return "service çalışıyor";
        }
    }
}
