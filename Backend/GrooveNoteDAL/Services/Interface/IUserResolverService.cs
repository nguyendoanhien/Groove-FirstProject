using System.Collections.Generic;
using System.Security.Claims;

namespace GrooveNoteDAL.Services.Interface
{
    public interface IUserResolverService
    {
        string CurrentUserName();

        IEnumerable<Claim> CurrentUserClaims();
    }
}