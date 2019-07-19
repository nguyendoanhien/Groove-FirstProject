using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using GrooveMessengerDAL.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GrooveMessengerAPI.Controllers
{
    public class ApiControllerBase : ControllerBase
    {
        protected readonly IUserResolverService userResolverService;

        public ApiControllerBase(IUserResolverService userResolverService)
        {
            this.userResolverService = userResolverService;
        }

        protected string CurrentUserName => userResolverService.CurrentUserName();
        protected Guid CurrentUserId => string.IsNullOrEmpty(userResolverService.CurrentUserId()) ? Guid.Empty : new Guid(userResolverService.CurrentUserId()) ;
        protected Guid CurrentUserInfoId => string.IsNullOrEmpty(userResolverService.CurrentUserInfoId()) ? Guid.Empty : new Guid(userResolverService.CurrentUserInfoId());

    }
}
