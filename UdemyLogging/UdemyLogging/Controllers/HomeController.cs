using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using UdemyLogging.Models;

namespace UdemyLogging.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILoggerFactory _loggerFactory;

        public HomeController(ILoggerFactory loggerFactory)
        {
            _loggerFactory=loggerFactory;
        }

        
        public IActionResult Index()
        {
            var _logger = _loggerFactory.CreateLogger("Home Controller");
            _logger.LogTrace("Index sayfasına girildi");
            _logger.LogDebug("ındex sayfasına girildi.");
            _logger.LogInformation("İndex sayfasına girildi");
            _logger.LogWarning("ındex sayfasına girildi.");
            _logger.LogCritical("index sayfasına girildi.");
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
