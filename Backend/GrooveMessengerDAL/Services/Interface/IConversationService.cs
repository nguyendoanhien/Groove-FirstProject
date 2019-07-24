using GrooveMessengerDAL.Models.CustomModel;
using System.Collections.Generic;

namespace GrooveMessengerDAL.Services.Interface
{
    public interface IConversationService
    {       
        IEnumerable<ChatModel> GetAllConversationOfAUser(string UserId);
        ChatModel GetConversationOfAUser(string ConversationId);
        void AddConversation(Models.Conversation.CreateConversationModel createMessageModel);
    }
}
