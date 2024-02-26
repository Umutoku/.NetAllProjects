using System.Net.Mail;
using System.Net.Mime;

namespace WebApp.ChainOfResponsibility.ChainOfResponsibility
{
    public class SendEmailProcessHandler<T> : ProcessHandler
    {
        private readonly string _email;
        private readonly string _fileName;
        public SendEmailProcessHandler(string email, string fileName)
        {
            _email = email;
            _fileName = fileName;
        }
        public override object Handle(object request)
        {
            var zipMemoryStream = request as MemoryStream;

            zipMemoryStream.Position = 0; // MemoryStream'in pozisyonunu sıfırlıyoruz çünkü MemoryStream'den okuma yapacağız.

            var mailMessage = new MailMessage();

            mailMessage.To.Add(_email);

            mailMessage.Subject = "Your file is ready!";

            mailMessage.Body = "Your file is ready to download.";

            Attachment attachment = new Attachment(zipMemoryStream,_fileName,MediaTypeNames.Application.Zip); // MemoryStream'i Attachment'a çeviriyoruz.

            mailMessage.Attachments.Add(attachment);

            mailMessage.IsBodyHtml = true; // Mail'in HTML formatında olmasını sağlıyoruz.

            var smtpClient = new SmtpClient("smtp.gmail.com", 587);


            smtpClient.Credentials = new System.Net.NetworkCredential("denemq@mail.cpm","asda");

            smtpClient.Send(mailMessage);

            return base.Handle(null); // null değerini ProcessHandler sınıfına gönderiyoruz. Çünkü artık bir sonraki işlem olmayacak.
        }
    }
}
