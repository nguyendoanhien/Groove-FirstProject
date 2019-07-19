using System;
using System.Collections.Generic;
using System.Text;

namespace GrooveMessengerDAL.Models.Participant
{
    public class ParticipantModel
    {
        public Guid Id { get; set; }
        public Guid ConvId { get; set; }
        public string UserId { get; set; }
    }
}
