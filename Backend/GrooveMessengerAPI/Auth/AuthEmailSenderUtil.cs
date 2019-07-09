using MailKit.Net.Smtp;
using MimeKit;

namespace GrooveMessengerAPI.Auth
{
    public interface IAuthEmailSenderUtil
    {
        void SendEmail(string userId, string ctoken, string toEmail, string fromEmail = "haunc97@gmail.com");
    }
    public class AuthEmailSenderUtil : IAuthEmailSenderUtil
    {
        public void SendEmail(string userId, string ctoken, string toEmail, string fromEmail = "haunc97@gmail.com")
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Groove messenger system", fromEmail));
            message.To.Add(new MailboxAddress("Groove messenger system", toEmail));
            message.Subject = "Password confirmation";
            message.Body = new TextPart("plain")
            {
                Text = "http://localhost:4200/#/email-confirmation?userId=" + userId + "&ctoken=" + ctoken
            };
            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587, false);
                client.Authenticate(fromEmail, "yourPass");
                client.Send(message);
                client.Disconnect(true);
            }
        }
    }
}
