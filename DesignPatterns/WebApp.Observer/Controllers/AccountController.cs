using WebApp.Observer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApp.Observer.Observer;
using MediatR;
using WebApp.Observer.Events;

namespace WebApp.Observer.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserObserverSubject _userObserverSubject;
        private readonly IMediator _mediator;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, UserObserverSubject userObserverSubject, IMediator mediator)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userObserverSubject = userObserverSubject;
            _mediator = mediator;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            var hasUser = await _userManager.FindByEmailAsync(email);

            if (hasUser != null)
            {
                var signInResult = await _signInManager.PasswordSignInAsync(hasUser, password, true, false); // true: Beni hatırla, false: hesap kilitlendiğinde oturumu kapat

                if (signInResult.Succeeded)
                {
                    return RedirectToAction(nameof(HomeController.Index), "Home");
                }
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(UserCreateViewModel user)
        {
            var appUser = new AppUser
            {
                UserName = user.UserName,
                Email = user.Email
            };

            var result = await _userManager.CreateAsync(appUser, user.Password); // identity result döner 

            if(result.Succeeded)
            {
                ViewBag.message = "User created";
                
                //_userObserverSubject.Notify(appUser); // Observer pattern kullanarak kullanıcı oluşturulduğunda gerekli olan işlemleri yapar
                await _mediator.Publish(new UserCreatedEvent() { AppUser = appUser} ); // MediatR kütüphanesi kullanarak kullanıcı oluşturulduğunda gerekli olan işlemleri yapar

            }

            ViewBag.message = result.Errors.FirstOrDefault().Description; // ilk hatayı al 
            return View();
        }
    }
}
