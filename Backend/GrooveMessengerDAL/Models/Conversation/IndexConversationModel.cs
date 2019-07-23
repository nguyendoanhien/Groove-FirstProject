using GrooveMessengerDAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GrooveMessengerDAL.Models.Conversation
{
    public class IndexConversationModel
    {
        public Guid Id { get; set; }
        public IEnumerable<MessageEntity> MessageEntity { get; set; }
    }
}
