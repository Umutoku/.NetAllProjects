using MediatR;
using System.Net.Mail;
using System.Net;
using WebApp.Observer.Events;
using WebApp.Observer.Observer;
using WebApp.Observer.Models;

namespace WebApp.Observer.EventHandlers
{
    public class SendEmailEventHandler : INotificationHandler<UserCreatedEvent>
    {
        private readonly ILogger<UserObserverSendEmail> _logger;

        public SendEmailEventHandler(ILogger<UserObserverSendEmail> logger)
        {
            _logger = logger;
        }

        public Task Handle(UserCreatedEvent notification, CancellationToken cancellationToken)
        {

            var mailMessage = new MailMessage();

            var smtpClient = new SmtpClient("gmail.com");

            mailMessage.From = new MailAddress("umut.oku@gmail.com");

            mailMessage.To.Add(new MailAddress(notification.AppUser.Email!));

            mailMessage.Subject = "Welcome to our website";

            mailMessage.Body = "Welcome to our website";

            smtpClient.Port = 587;

            smtpClient.Credentials = new NetworkCredential("umut.oku@gmail.com", "123");

            smtpClient.Send(mailMessage);

            _logger.LogInformation($"Email sent to user: {notification.AppUser.UserName}");

            return Task.CompletedTask;
        }
    }
}
