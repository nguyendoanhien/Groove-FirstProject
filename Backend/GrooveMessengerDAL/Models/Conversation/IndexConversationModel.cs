using System;
using System.Collections.Generic;
using GrooveMessengerDAL.Entities;

namespace GrooveMessengerDAL.Models.Conversation
{
    public class IndexConversationModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public string Avatar { get; set; }
        public bool IsGroup { get; set; }
        public IEnumerable<MessageEntity> MessageEntity { get; set; }
    }
}