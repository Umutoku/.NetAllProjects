using System.Net;
using System.Net.Mail;
using WebApp.Observer.Models;

namespace WebApp.Observer.Observer
{
    public class UserObserverSendEmail : IUserObserver
    {
        private readonly IServiceProvider _serviceProvider;

        public UserObserverSendEmail(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void UserCreated(AppUser user)
        {
            var logger = _serviceProvider.GetRequiredService<ILogger<UserObserverSendEmail>>(); 

            var mailMessage = new MailMessage();

            var smtpClient = new SmtpClient("gmail.com");

            mailMessage.From = new MailAddress("umut.oku@gmail.com");

            mailMessage.To.Add(user.Email);

            mailMessage.Subject = "Welcome to our website";

            mailMessage.Body = "Welcome to our website";

            smtpClient.Port = 587;

            smtpClient.Credentials = new NetworkCredential("umut.oku@gmail.com", "123");

            smtpClient.Send(mailMessage);

            logger.LogInformation($"Email sent to user: {user.UserName}");
        }
    }
}
