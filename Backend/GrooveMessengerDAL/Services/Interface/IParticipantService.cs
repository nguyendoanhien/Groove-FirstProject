using GrooveMessengerDAL.Entities;
using GrooveMessengerDAL.Models.Participant;
using System;
using System.Collections.Generic;

namespace GrooveMessengerDAL.Services.Interface
{
    public interface IParticipantService
    {
        IEnumerable<Guid> GetAllConversationIdOfAUser(string UserId);
        void NewParticipant(ParticipantModel participantModel);
    }
}
