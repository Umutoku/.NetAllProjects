using AspNetCoreIdentityApp.Web.Localization;
using AspNetCoreIdentityApp.Web.Models;
using Microsoft.AspNetCore.Identity;

namespace AspNetCoreIdentityApp.Web.Extensions
{
    public static class StartupExtensions
    {
        public static void AddIdentityWithExt(this IServiceCollection services)
        {
            services.AddIdentity<AppUser, AppRole>(options =>
            {
                options.User.RequireUniqueEmail = true; // Email adreslerinin unique olmasını sağlar
                options.User.AllowedUserNameCharacters = "abcçdefgğhıijklmnoöprsştuüvyzABCÇDEFGĞHİIJKLMNOÖPRSŞTUÜVYZ0123456789-._"; // Kullanıcı adında kullanılabilir karakterleri belirler
                options.Password.RequireDigit = false; // Şifrelerde rakam zorunluluğunu kaldırır
                options.Password.RequireLowercase = false; // Şifrelerde küçük harf zorunluluğunu kaldırır
                options.Password.RequireUppercase = false; // Şifrelerde büyük harf zorunluluğunu kaldırır
                options.Password.RequireNonAlphanumeric = false; // Şifrelerde alfanumerik karakter zorunluluğunu kaldırır
                options.Password.RequiredLength = 4; // Şifrelerin minimum uzunluğunu belirler
                options.Lockout.MaxFailedAccessAttempts = 3; // Kullanıcı 3 defa yanlış giriş yaparsa hesabı kilitler
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5); // Hesap kilitlendiğinde 5 dakika sonra açılmasını sağlar
                options.Lockout.AllowedForNewUsers = true; // Yeni kullanıcılar içinde hesap kilitlemeyi aktif eder

            }).AddEntityFrameworkStores<AppDbContext>() // Identity verilerinin nerede saklanacağını belirler
            .AddPasswordValidator<PasswordValidator<AppUser>>()
            .AddErrorDescriber<LocalizationIdentityErrorDescriber>()   
            .AddUserValidator<UserValidator<AppUser>>(); // Custom validatorları ekler
            ;
        }
    }
}
