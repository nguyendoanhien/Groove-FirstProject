using GrooveMessengerAPI.Areas.Chat.Models;
using GrooveMessengerAPI.Areas.Chat.Models.Contact;
using GrooveMessengerDAL.Models.Contact;
using System.Threading.Tasks;

namespace GrooveMessengerAPI.Hubs.Utils
{
    public interface IContactHubClient
    {
        Task SendNewContactToFriend(string msg);
        Task SendRemoveContactToFriend(string msg);
        Task AddNewContact(AddContactModel fromUserContact);
        //HubContact fromUserContact
    }
}
