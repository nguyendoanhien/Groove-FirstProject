using System;
using System.Collections.Generic;
using GrooveMessengerDAL.Models.Participant;

namespace GrooveMessengerDAL.Services.Interface
{
    public interface IParticipantService
    {
        IEnumerable<Guid> GetAllConversationIdOfAUser(string userId);
        void AddParticipant(ParticipantModel participantModel);
    }
}