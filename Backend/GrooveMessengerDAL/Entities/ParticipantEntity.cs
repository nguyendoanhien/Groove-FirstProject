using GrooveMessengerDAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GrooveMessengerDAL.Entities
{
    public class ParticipantEntity : AuditBaseEntity<Guid>
    {

        public Guid ConversationId { get; set; }


        public string UserId { get; set; }

        public int Status { get; set; }

        public ConversationEntity ConversationEntity { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
