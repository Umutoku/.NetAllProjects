using BaseProject.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApp.Adapter.Services;

namespace BaseProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IImageProcess _imageProcess;
        private readonly IAdvanceImageProcess _advanceImageProcess;

        public HomeController(ILogger<HomeController> logger, IImageProcess imageProcess, IAdvanceImageProcess advanceImageProcess)
        {
            _logger = logger;
            _imageProcess = imageProcess;
            _advanceImageProcess = advanceImageProcess;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult AddWatermark()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddWatermark(string text, IFormFile file)
        {
            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                _imageProcess.AddWatermark(text, file.FileName, memoryStream);
            }

            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
