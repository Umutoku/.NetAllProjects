namespace AspNetCoreIdentityApp.Web.Services
{
    public interface IEmailService
    {
        Task SendResetPasswordEmail(string resetLink, string toEmail);
    }
}
