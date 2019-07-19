using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using GrooveMessengerDAL.Services.Interface;
using GrooveMessengerDAL.Entities;
using System;
using System.Linq;

namespace GrooveMessengerDAL.Services
{
    public class UserResolverService : IUserResolverService
    {
        private readonly IHttpContextAccessor _context;
        public UserResolverService(IHttpContextAccessor context)
        {
            _context = context;
        }

        public string CurrentUserName()
        {
            return _context.HttpContext.User?.Identity?.Name;
        }

        public IEnumerable<Claim> CurrentUserClaims()
        {
            return _context.HttpContext.User.Claims;
        }

        public string CurrentUserId()
        {
            return _context.HttpContext.User?.Claims.FirstOrDefault(x => x.Type == "UserId").Value;
        }
        public string CurrentUserInfoId()
        {
            return _context.HttpContext.User?.Claims.FirstOrDefault(x => x.Type == "UserInfoId").Value;
        }
    }
}
