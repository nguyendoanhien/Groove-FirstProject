using GrooveMessengerDAL.Entities;
using System;
using System.Collections.Generic;

namespace GrooveMessengerDAL.Services.Interface
{
    public interface IParticipantService
    {
        IEnumerable<Guid> GetAllConversationIdOfAUser(string UserId);
    }
}
