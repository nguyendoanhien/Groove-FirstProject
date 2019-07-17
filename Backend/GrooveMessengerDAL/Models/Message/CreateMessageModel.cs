using System;
using System.Collections.Generic;
using System.Text;

namespace GrooveMessengerDAL.Models.Message
{
    public class CreateMessageModel
    {
        public Guid ConversationId { get; set; }

        public string SenderId { get; set; }

        public string SeenBy { get; set; }

        public string Content { get; set; }

        public string Type { get; set; }
    }
}
