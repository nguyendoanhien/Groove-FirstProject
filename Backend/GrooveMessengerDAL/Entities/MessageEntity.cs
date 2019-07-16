using GrooveMessengerDAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GrooveMessengerDAL.Entities
{
    public class MessageEntity : AuditBaseEntity<Guid>
    {

        public Guid ConversationId { get; set; }

        public string SenderId { get; set; }

        public string SeenBy { get; set; }

        public string Content { get; set; }

        public string Type { get; set; }

        public virtual ConversationEntity ConversationEntity { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
