using System;
using GrooveMessengerDAL.Services.Interface;
using Microsoft.AspNetCore.Mvc;

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

        protected Guid CurrentUserId => string.IsNullOrEmpty(userResolverService.CurrentUserId())
            ? Guid.Empty
            : new Guid(userResolverService.CurrentUserId());

        protected Guid CurrentUserInfoId => string.IsNullOrEmpty(userResolverService.CurrentUserInfoId())
            ? Guid.Empty
            : new Guid(userResolverService.CurrentUserInfoId());
    }
}