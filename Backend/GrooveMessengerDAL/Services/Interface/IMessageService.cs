using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GrooveMessengerDAL.Entities;
using GrooveMessengerDAL.Models.CustomModel;
using GrooveMessengerDAL.Models.Message;

namespace GrooveMessengerDAL.Services.Interface
{
    public interface IMessageService
    {
        EditMessageModel GetMessageForEdit(Guid id);
        Task<EditMessageModel> GetMessageForEditAsync(Guid id);
        void EditMessageModel(EditMessageModel data);
        bool CheckExisting(Guid id);
        void DeleteMessage(Guid id);
        IEnumerable<MessageEntity> LoadMoreMessages(int pageNumber, int pageSize);
        void AddMessage(CreateMessageModel msg);
        Task<IndexMessageModel> AddMessageAsync(CreateMessageModel msg);
        MessageEntity GetMessageById(Guid id);
        int SetValueSeenBy(string userId, Guid conversationId);
        IEnumerable<DialogModel> GetDialogs(Guid conversationId);
        int GetUnreadMessages(Guid conversationId, string userId);
    }
}