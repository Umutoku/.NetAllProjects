using BaseProject.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApp.Strategy.Models;

namespace WebApp.Strategy.Controllers
{
    [Authorize]
    public class SettingsController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public SettingsController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            Settings settings = new Settings();
            if(User.Claims.Where(x=>x.Type == Settings.claimDatabaseType).FirstOrDefault() == null)
            {
                settings.DatabaseType = (EDatabaseType) int.Parse(User.Claims.First(x=>x.Type == Settings.claimDatabaseType).Value); // claim'den database type alınır.
            }
            else
            {
                settings.DatabaseType = settings.GetDefaultDatabase;
            }

            return View(settings);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeDatabase(int databaseType)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name); // kullanıcı adı ile kullanıcı bulunur.

            var newClaim = new Claim(Settings.claimDatabaseType, databaseType.ToString()); // sayesinde claim'e database type eklenir.

            var claims = await _userManager.GetClaimsAsync(user); // kullanıcının claim'leri alınır.

            var hasDatabaseTypeClaim = claims.Where(x => x.Type == Settings.claimDatabaseType).FirstOrDefault(); // kullanıcının database type claim'i alınır.

            if (hasDatabaseTypeClaim != null) // eğer kullanıcının database type claim'i varsa
            {
                await _userManager.ReplaceClaimAsync(user, hasDatabaseTypeClaim, newClaim); // database type claim'i güncellenir.
            }
            else
            {
                await _userManager.AddClaimAsync(user, newClaim); // database type claim'i eklenir.
            }

            await _signInManager.SignOutAsync(); // kullanıcı oturumu kapatılır.

            var authenticateResult = await HttpContext.AuthenticateAsync(); // kullanıcı bilgileri alınır.

            await _signInManager.SignInAsync(user, authenticateResult.Properties); // kullanıcı oturumu açılır.

            return RedirectToAction(nameof(Index));
        }
    }
}
