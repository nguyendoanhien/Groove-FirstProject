using System.Collections.Generic;
using GrooveMessengerDAL.Models.Conversation;
using GrooveMessengerDAL.Models.CustomModel;
using GrooveMessengerDAL.Models.PagingModel;

namespace GrooveMessengerDAL.Services.Interface
{
    public interface IConversationService
    {
        IEnumerable<ChatModel> GetAllConversationOfAUser(string userId);
        void AddConversation(Models.Conversation.CreateConversationModel createMessageModel);
        ChatModel GetConversationById(string conversationId, PagingParameterModel pagingParameterModel);
        IEnumerable<DialogModel> GetAllConversationOfAUserDraft(string userId = null);
        void AddConversation();
    }
}