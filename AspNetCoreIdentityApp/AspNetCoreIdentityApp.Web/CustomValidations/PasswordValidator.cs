using AspNetCoreIdentityApp.Web.Models;
using Microsoft.AspNetCore.Identity;

namespace AspNetCoreIdentityApp.Web.CustomValidations
{
    public class PasswordValidator : IPasswordValidator<AppUser>
    {
        public Task<IdentityResult> ValidateAsync(UserManager<AppUser> manager, AppUser user, string? password)
        {
            var errors = new List<IdentityError>();

            if(password!.ToLower().Contains(user.UserName!.ToLower()))
            {
                return Task.FromResult(IdentityResult.Failed(new IdentityError { Code = "PasswordContainsUserName", Description = "Şifre alanı kullanıcı adı içeremez" }));
            }

            if(password.StartsWith("1234"))
            {
                return Task.FromResult(IdentityResult.Failed(new IdentityError { Code = "PasswordContains1234", Description = "Şifre alanı ardışık sayı içeremez" }));
            }

            if(password.EndsWith("1234"))
            {
                return Task.FromResult(IdentityResult.Failed(new IdentityError { Code = "PasswordContains1234", Description = "Şifre alanı ardışık sayı içeremez" }));
            }

            if(errors.Any())
            {
                return Task.FromResult(IdentityResult.Failed(errors.ToArray()));
            }


            return Task.FromResult(IdentityResult.Success);
        }
    }
}
