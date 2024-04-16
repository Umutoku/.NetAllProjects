using System.ComponentModel.DataAnnotations;

namespace AspNetCoreIdentityApp.Web.ViewModels
{
    public class SignUpViewModel
    {
        public SignUpViewModel()
        {
            
        }
        public SignUpViewModel(string userName, string email, string phoneNumber, string password)
        {
            UserName = userName;
            Email = email;
            PhoneNumber = phoneNumber;
            Password = password;
        }
        [Required(ErrorMessage = "User Name is required!")]
        [Display(Name = "User Name: ")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Email is required!")]
        [Display(Name = "Email: ")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Phone Number is required!")]
        [Display(Name = "Phone Number: ")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Password is required!")]
        [Display(Name = "Password: ")]
        public string Password { get; set; }
        [Compare(nameof(Password), ErrorMessage = "Passwords do not match!")]
        [Required(ErrorMessage = "Confirm Password is required!")]
        [Display(Name = "Confirm Password: ")]
        public string ConfirmPassword { get; set; }
    }
}
