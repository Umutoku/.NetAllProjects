using EntityFrameworkCore2.Models;
using Microsoft.AspNetCore.Mvc;

namespace EntityFrameworkCore2.Controllers
{
    public class HomeController : Controller
    {
        private IProductRepository repository;

        public HomeController(IProductRepository repo)
        {
            repository = repo;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult list() => View(repository.Product);
    }
}
