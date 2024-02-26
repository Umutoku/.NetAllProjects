using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Encodings.Web;
using XSS.Web.Models;

namespace XSS.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private HtmlEncoder _htmlEncoder;
        private JavaScriptEncoder _javaScriptEncoder;
        private UrlEncoder _urlEncoder;

        public HomeController(ILogger<HomeController> logger, HtmlEncoder htmlEncoder, JavaScriptEncoder javaScriptEncoder, UrlEncoder urlEncoder)
        {
            _logger = logger;
            _htmlEncoder = htmlEncoder;
            _javaScriptEncoder = javaScriptEncoder;
            _urlEncoder = urlEncoder;
        }
        [ValidateAntiForgeryToken] // bu attribute ile formun token kontrolü yapılır 
        [IgnoreAntiforgeryToken] // bu attribute ile formun token kontrolü yapılmaz
        [HttpPost]
        public IActionResult CommentAdd(string name, string comment)
        {
            string encodeName = _urlEncoder.Encode(name); // url için encode zararsız hale getirir

            string encodeComment = _htmlEncoder.Encode(comment); // html için encode zararsız hale getirir

            string encodeJavaScript = _javaScriptEncoder.Encode(comment); // javascript için encode zararsız hale getirir 

            ViewBag.Name = name;
            ViewBag.Comment = comment;
            System.IO.File.AppendAllText("comment.txt", $"{name} : {comment}\n");
            return RedirectToAction("CommentAdd");
        }
        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            string? returnUrl = TempData["returnUrl"] as string;

            //email ve password kontrolü yapılır

            if (Url.IsLocalUrl(returnUrl)) // local url ise yönlendirme yapılır bu kod sayesinde sadece site içi yönlendirme yapılır
                return Redirect(returnUrl);

            return Redirect("/");
        }
        public IActionResult Login(string returnUrl = "/")
        {
            TempData["returnUrl"] = returnUrl;
            return View();
        }

        public IActionResult CommentAdd()
        {
            if (System.IO.File.Exists("comment.txt"))
                ViewBag.Comments = System.IO.File.ReadAllLines("comment.txt");
            return View();
        }

        public IActionResult Index()
        {
            HttpContext.Response.Cookies.Append("XSS", "XSS");


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
