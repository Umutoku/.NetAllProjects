using AspNetCoreIdentityApp.Web.Extensions;
using AspNetCoreIdentityApp.Web.Models;
using AspNetCoreIdentityApp.Web.Services;
using AspNetCoreIdentityApp.Web.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AspNetCoreIdentityApp.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IEmailService _emailService;

        public HomeController(ILogger<HomeController> logger, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IEmailService emailService)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult SignUp()
        {
            return View();
        }

        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(SignInViewModel model, string? returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Action("Index", "Home");

            if (ModelState.IsValid)
            {
                AppUser user = await _userManager.FindByEmailAsync(model.Email);

                if (user != null)
                {
                    await _signInManager.SignOutAsync();

                    var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, true);

                    if (result.Succeeded)
                    {
                        return Redirect(returnUrl);
                    }
                    ModelState.AddModelError("", "Email or password is invalid");

                    if (result.IsLockedOut)
                    {
                        ModelState.AddModelError("", "Your account has been locked. Please try again later.");
                    }

                }

                ModelState.AddModelError("", $"Email or password is invalid{await _userManager.GetAccessFailedCountAsync(user)}");
                return View(model);
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpViewModel model)
        {
            if (ModelState.IsValid)
            {
                AppUser user = new AppUser
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber
                };

                IdentityResult result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    TempData["SuccessMessage"] = "User created successfully!"; // cookiede tek seferlik veri saklar
                    return RedirectToAction(nameof(SignUp));
                }
                else
                {
                    ModelState.AddModelErrorList(result.Errors.ToList()); // ModelStateExtensions ile oluşturulan metot
                }
            }

            return View(model);
        }

        public IActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordViewModel request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                ModelState.AddModelError("", "User not found!");
                return View(request);
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var passwordResetLink = Url.Action("ResetPassword", "Home", new { userId = user.Id, token }, Request.Scheme);

            //Email Service

            await _emailService.SendResetPasswordEmail(passwordResetLink, request.Email);

            TempData["SuccessMessage"] = "Şifre sıfırlama linki mail adresinize gönderilmiştir.";

            return RedirectToAction(nameof(ForgetPassword));
        }

        public IActionResult ResetPassword(string userId, string token)
        {
            TempData["userId"] = userId;
            TempData["token"] = token;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            string userId = TempData["userId"] as string;
            string token = TempData["token"] as string;

            if(userId == null || token == null)
            {
                ModelState.AddModelError("", "Invalid token or user id!");
                return View(model);
            }

            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(userId);

                if (user == null)
                {
                    ModelState.AddModelError("", "User not found!");
                    return View(model);
                }

                var result = await _userManager.ResetPasswordAsync(user, token, model.Password);

                if (result.Succeeded)
                {
                    TempData["SuccessMessage"] = "Password reset successfully!";
                    return RedirectToAction(nameof(SignIn));
                }
                else
                {
                    ModelState.AddModelErrorList(result.Errors.ToList());
                }

                return View(model);
            }

            return View(model);
            
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
