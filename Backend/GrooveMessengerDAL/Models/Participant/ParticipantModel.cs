using System;

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