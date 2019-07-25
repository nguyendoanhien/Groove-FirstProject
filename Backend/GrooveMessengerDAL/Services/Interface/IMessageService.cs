using GrooveMessengerDAL.Entities;
using GrooveMessengerDAL.Models.CustomModel;
using GrooveMessengerDAL.Models.Message;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GrooveMessengerDAL.Services.Interface
{
    public interface IMessageService
    {
        EditMessageModel GetMessageForEdit(Guid id);
        Task<EditMessageModel> GetMessageForEditAsync(Guid id);
        void EditMessageModel(EditMessageModel data);
        bool CheckExisting(Guid id);
        void DeleteMessage(Guid id);
        IEnumerable<MessageEntity> loadMoreMessages(int pageNumber, int pageSize);
        void AddMessage(CreateMessageModel msg);
        Task<IndexMessageModel> AddMessageAsync(CreateMessageModel msg);
        MessageEntity GetMessageById(Guid id);
        int SetValueSeenBy(string userId, Guid conversationId);
        IEnumerable<DialogModel> GetDialogs(Guid ConversationId);
        int GetUnreadMessages(Guid conversationId, string userId);
    }
}
