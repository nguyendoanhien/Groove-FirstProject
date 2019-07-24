using System;
using System.Collections.Generic;
using System.Text;

namespace GrooveMessengerDAL.Models.Participant
{
    public class ParticipantModel
    {
        public Guid Id { get; set; }
      
        public string UserId { get; set; }
        public Guid ConversationId { get; set; }
        public int Status { get; set; }
    }
}
