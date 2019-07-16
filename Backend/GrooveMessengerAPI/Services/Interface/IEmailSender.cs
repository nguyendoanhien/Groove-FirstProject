using System.Threading.Tasks;

namespace GrooveMessengerAPI.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
