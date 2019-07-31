using System.Collections.Generic;
using GrooveMessengerDAL.Models.Conversation;
using GrooveMessengerDAL.Models.CustomModel;

namespace GrooveMessengerDAL.Services.Interface
{
    public interface IConversationService
    {
        IEnumerable<ChatModel> GetAllConversationOfAUser(string userId);
        void AddConversation(Models.Conversation.CreateConversationModel createMessageModel);
        ChatModel GetConversationById(string ConversationId);
        IEnumerable<DialogModel> GetAllConversationOfAUserDraft(string UserId = null);
        void AddConversation();
    }
}