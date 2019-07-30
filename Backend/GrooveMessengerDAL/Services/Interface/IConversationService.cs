using System.Collections.Generic;
using GrooveMessengerDAL.Models.Conversation;
using GrooveMessengerDAL.Models.CustomModel;

namespace GrooveMessengerDAL.Services.Interface
{
    public interface IConversationService
    {
        IEnumerable<ChatModel> GetAllConversationOfAUser(string userId);
        void AddConversation(Models.Conversation.CreateConversationModel createMessageModel);
        ChatModel GetConversationById(string conversationId);
        IEnumerable<DialogDraftModel> GetAllConversationOfAUserDraft(string userId = null);
        void AddConversation();
    }
}