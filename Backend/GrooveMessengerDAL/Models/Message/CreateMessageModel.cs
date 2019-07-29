using System;

namespace GrooveMessengerDAL.Models.Message
{
    public class CreateMessageModel
    {
        public Guid ConversationId { get; set; }

        public string SenderId { get; set; }

        public string SeenBy { get; set; }

        public string Content { get; set; }

        public string Type { get; set; }

        public string Receiver { get; set; } // Not mapping to MessageEntity
    }
}