using GrooveMessengerAPI.Areas.Chat.Models;
using GrooveMessengerAPI.Areas.Chat.Models.Contact;
using System.Threading.Tasks;

namespace GrooveMessengerAPI.Hubs.Utils
{
    public interface IContactHubClient
    {
        Task SendNewContactToFriend(HubContact fromUserContact);
        Task SendRemoveContact(HubContact contact);
    }
}
