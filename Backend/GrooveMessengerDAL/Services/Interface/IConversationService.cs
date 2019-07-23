using GrooveMessengerDAL.Models.CustomModel;
using System.Collections.Generic;

namespace GrooveMessengerDAL.Services.Interface
{
    public interface IConversationService
    {
        IEnumerable<ChatModel> GetAllConversationOfAUser(string UserId = null);
        IEnumerable<DialogDraftModel> GetAllConversationOfAUserDraft(string UserId = null);
        void AddConversation();
    }
}
