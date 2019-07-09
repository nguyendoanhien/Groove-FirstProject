using GrooveMessengerAPI.Areas.Identity.Models;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;

namespace GrooveMessengerAPI.Auth
{
    public interface IAuthEmailSenderUtil
    {
        void SendEmail(string body, string subject, string toEmail, IConfiguration config);
    }
    public class AuthEmailSenderUtil : IAuthEmailSenderUtil
    {
        public void SendEmail(string body, string subject, string toEmail, IConfiguration config)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Groove messenger system", config["AuthenticationEmailSender:EmailAddress"]));
            message.To.Add(new MailboxAddress("Groove messenger system", toEmail));
            message.Subject = subject;
            message.Body = new TextPart("plain")
            {
                Text = body
            };
            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587, false);
                client.Authenticate(config["AuthenticationEmailSender:EmailAddress"], config["AuthenticationEmailSender:Password"]);
                client.Send(message);
                client.Disconnect(true);
            }
        }
    }
}
