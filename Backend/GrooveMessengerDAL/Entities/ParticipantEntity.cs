using System;
using GrooveMessengerDAL.Models;

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