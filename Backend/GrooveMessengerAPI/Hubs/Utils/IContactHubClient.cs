using GrooveMessengerAPI.Areas.Chat.Models;
using GrooveMessengerAPI.Areas.Chat.Models.Contact;
using GrooveMessengerDAL.Models.Contact;
using GrooveMessengerDAL.Models.User;
using System.Threading.Tasks;

namespace GrooveMessengerAPI.Hubs.Utils
{
    public interface IContactHubClient
    {
        Task SendNewContactToFriend(IndexUserInfoModel contact, ContactChatList contactChatList,object dialog);
    }
}
