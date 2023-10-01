
using SendGrid.Helpers.Mail;
using SendGrid;

namespace Hangfire.Web.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration _configuration;

        public EmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task Sender(string userId, string message)
        {
            // bu userId sahip kullanıcı bul ve email al


            var apiKey = _configuration.GetSection("APIs")["SendGridApi"];
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("kocasinan@ibuyuk.com", "Sinan Kocatekin");
            var subject = "Sending with SendGrid is Fun";
            var to = new EmailAddress("umut.oku@gmail.com", "Umut Oku");
            //var plainTextContent = "and easy to do anywhere, even with C#";
            var htmlContent = $"<strong>{message}</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, null, htmlContent);
            await client.SendEmailAsync(msg);
        }
    }
}
