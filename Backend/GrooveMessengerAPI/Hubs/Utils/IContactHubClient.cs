using System.Threading.Tasks;
using GrooveMessengerAPI.Areas.Chat.Models;
using GrooveMessengerDAL.Models.Conversation;
using GrooveMessengerDAL.Models.CustomModel;
using GrooveMessengerDAL.Models.User;

namespace GrooveMessengerAPI.Hubs.Utils
{
    public interface IContactHubClient
    {
        Task SendNewContactToFriend(IndexUserInfoModel contact, ContactChatList contactChatList, object dialog);

        Task BroadcastNewGroupToFriends(GroupConversationModel group);
        Task EditConversationToFriends(EditConversationModel editConversation);
    }
}