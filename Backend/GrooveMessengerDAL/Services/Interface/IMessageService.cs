using GrooveMessengerDAL.Entities;
using GrooveMessengerDAL.Models.Message;
using System;
using System.Collections.Generic;
using System.Text;

namespace GrooveMessengerDAL.Services.Interface
{
    public interface IMessageService
    {
        IEnumerable<MessageEntity> loadMoreMessages(int pageNumber, int pageSize);
        void AddMessage(CreateMessageModel msg);
        MessageEntity GetMessageById(Guid id);
        void UpdateStatusMessage(Guid Id);
    }
}
