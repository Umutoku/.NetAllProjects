using Microsoft.AspNetCore.Mvc;

namespace DbFirst.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
