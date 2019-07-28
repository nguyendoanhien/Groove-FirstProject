using System.Threading.Tasks;
using GrooveMessengerAPI.Areas.Chat.Models;
using GrooveMessengerDAL.Models.User;

namespace GrooveMessengerAPI.Hubs.Utils
{
    public interface IContactHubClient
    {
        Task SendNewContactToFriend(IndexUserInfoModel contact, ContactChatList contactChatList, object dialog);
    }
}