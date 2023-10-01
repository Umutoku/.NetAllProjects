using Hangfire.Web.BackgroundJobs;
using Hangfire.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Hangfire.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
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
        [HttpPost]
        public IActionResult SignUp()
        {
            //üye kayıt işlemi 
            FireAndForgetJobs.EmailSendToUserJob("1234", "Sitemize hoş geldiniz");
            return View();

        }

        public IActionResult PictureSave()
        {
            BackgroundJobs.RecurringJobs.ReportingJob();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> PictureSave(IFormFile picture) 
        {
            string newFileName = string.Empty;
            if(picture!= null && picture.Length>0)
            {
                newFileName = Guid.NewGuid().ToString()+Path.GetExtension(picture.FileName);

                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/pictures",newFileName);

                using(var stream = new FileStream(path,FileMode.Create))
                {
                    await picture.CopyToAsync(stream);
                }

                string jobID = BackgroundJobs.DelayedJobs.AddWatermarkJob(newFileName,"www.umutoku.com");

                BackgroundJobs.ContinuationsJobs.WriteWatermarkStatusJob(jobID,newFileName);
            }
            return View();
        }
    }
}
