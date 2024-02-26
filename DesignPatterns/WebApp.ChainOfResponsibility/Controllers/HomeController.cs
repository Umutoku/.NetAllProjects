using WebApp.ChainOfResponsibility.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using WebApp.ChainOfResponsibility.ChainOfResponsibility;

namespace WebApp.ChainOfResponsibility.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppIdentityDbContext _context;

        public HomeController(ILogger<HomeController> logger, AppIdentityDbContext context)
        {
            _logger = logger;
            _context = context;
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

        public async Task<IActionResult> SendEmail()
        { 
            var products = await _context.Products.ToListAsync();

            var processHandler = new ExcelProcessHandler<Product>(); // Excel dosyası oluşturacak ProcessHandler sınıfını oluşturuyoruz.

            var processHandler2 = new ZipFileProcessHandler<Product>(); // Zip dosyası oluşturacak ProcessHandler sınıfını oluşturuyoruz.

            var processHandler3 = new SendEmailProcessHandler<Product>("gonderbuna@gmail.com","Products.zip"); // Mail gönderecek ProcessHandler sınıfını oluşturuyoruz.

            processHandler.SetNext(processHandler2); // ExcelProcessHandler'ın bir sonraki işlemi ZipFileProcessHandler olacak şekilde ayarlıyoruz.

            processHandler2.SetNext(processHandler3); // ZipFileProcessHandler'ın bir sonraki işlemi SendEmailProcessHandler olacak şekilde ayarlıyoruz.

            processHandler.Handle(products); // ProcessHandler zincirini çalıştırıyoruz.

            return View();


        }
    }
}
