using System.Collections.Generic;
using GrooveMessengerDAL.Models.Conversation;
using GrooveMessengerDAL.Models.CustomModel;

namespace GrooveMessengerDAL.Services.Interface
{
    public interface IConversationService
    {
        IEnumerable<ChatModel> GetAllConversationOfAUser(string UserId);
        ChatModel GetConversationOfAUser(string ConversationId);
        void AddConversation(CreateConversationModel createMessageModel);
        ChatModel GetConversationById(string ConversationId = null);
        IEnumerable<DialogDraftModel> GetAllConversationOfAUserDraft(string UserId = null);
        void AddConversation();
    }
}