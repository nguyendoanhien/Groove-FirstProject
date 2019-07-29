using System;

namespace GrooveMessengerDAL.Models.Message
{
    public class IndexMessageModel
    {
        public Guid Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public Guid ConversationId { get; set; }

        public string SenderId { get; set; }

        public string SeenBy { get; set; }

        public string Content { get; set; }

        public string Type { get; set; }
    }
}