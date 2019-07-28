using GrooveMessengerDAL.Models.CustomModel;
using System.Collections.Generic;

namespace GrooveMessengerDAL.Services.Interface
{
    public interface IConversationService
    {       
        IEnumerable<ChatModel> GetAllConversationOfAUser(string UserId);
        void AddConversation(Models.Conversation.CreateConversationModel createMessageModel);
        ChatModel GetConversationById(string ConversationId);
        IEnumerable<DialogDraftModel> GetAllConversationOfAUserDraft(string UserId = null);
        void AddConversation();
    }
}
