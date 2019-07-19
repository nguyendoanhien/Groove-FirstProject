using System.Collections.Generic;
using System.Security.Claims;

namespace GrooveMessengerDAL.Services.Interface
{
    public interface IUserResolverService
    {
        string CurrentUserName();

        IEnumerable<Claim> CurrentUserClaims();

        string CurrentUserId();
        string CurrentUserInfoId();

    }
}