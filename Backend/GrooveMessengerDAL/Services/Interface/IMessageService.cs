using GrooveMessengerDAL.Entities;
using GrooveMessengerDAL.Models.Message;
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
        MessageEntity GetMessageById(Guid id);
        void UpdateStatusMessage(Guid Id);
    }
}
