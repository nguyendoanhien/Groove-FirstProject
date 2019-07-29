using System;
using System.Collections.Generic;
using GrooveMessengerDAL.Models.Participant;

namespace GrooveMessengerDAL.Services.Interface
{
    public interface IParticipantService
    {
        IEnumerable<Guid> GetAllConversationIdOfAUser(string UserId);
        void AddParticipant(ParticipantModel participantModel);
    }
}