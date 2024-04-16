using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SampleProject.Web.Models;
using SampleProject.Web.Models.ViewModels;
using SampleProject.Web.Services;
using System.Diagnostics;

namespace SampleProject.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly AppDbContext _context;
        private readonly FileService fileService;

        public HomeController(ILogger<HomeController> logger, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, AppDbContext context, FileService fileService)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            this.fileService = fileService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult SignIn()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> SignIn(SignInViewModel model)
        {
            if (ModelState.IsValid)
            {
                var hasUser = await _userManager.FindByEmailAsync(model.Email);

                if (hasUser == null)
                {
                    ModelState.AddModelError("", "Invalid login attempt");
                    return View(model);
                }

                var result = await _signInManager.PasswordSignInAsync(hasUser, model.Password, false, false);

                if (result.Succeeded)
                {
                    return RedirectToAction("ProductList");
                }

                ModelState.AddModelError("", "Invalid login attempt");
            }

            return View(model);
        }

        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userToCreate = new IdentityUser
                {
                    UserName = model.Email,
                    Email = model.Email
                };

                var result = await _userManager.CreateAsync(userToCreate, model.Password);

                if (result.Succeeded)
                {
                    return RedirectToAction("SignIn");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }

            }

            return View(model);
        }

        public async Task<IActionResult> ProductList()
        {
            var user = await _userManager.GetUserAsync(User);

            if(_context.Products.Any(x=>x.UserId == user.Id)) 
                {
                var products = _context.Products.Where(x => x.UserId == user.Id).ToList();
                return View(products);
            }

            if (user == null)
            {
                return RedirectToAction("SignIn");
            }
            var product1 = new Product
            {
                Id = 1,
                Name = "Product 1",
                Price = 100,
                Description = "Product 1 Description",
                UserId = user.Id
            };

            var product2 = new Product
            {
                Id = 2,
                Name = "Product 2",
                Price = 200,
                Description = "Product 2 Description",
                UserId = user.Id
            };

            var product3 = new Product
            {
                Id = 3,
                Name = "Product 3",
                Price = 300,
                Description = "Product 3 Description",
                UserId = user.Id
            };

            var productsList = new List<Product> { product1, product2, product3 };

            _context.Products.AddRange(productsList);

            return View(productsList);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> CreateExcel()
        {

            var response = new
            {
                Status = await fileService.AddMessageToQueue()
            };

            return Json(response);

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
