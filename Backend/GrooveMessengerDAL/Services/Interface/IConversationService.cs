using GrooveMessengerDAL.Models.Conversation;
using GrooveMessengerDAL.Models.CustomModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace GrooveMessengerDAL.Services.Interface
{
    public interface IConversationService
    {
        void AddConversation(CreateConversationModel createConversation);
        IEnumerable<ChatModel> GetAllConversationOfAUser(string UserId);
        ChatModel GetConversationOfAUser(string ConversationId);
    }
}
