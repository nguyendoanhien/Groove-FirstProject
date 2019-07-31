using System;
using GrooveMessengerDAL.Services.Interface;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GrooveMessengerAPI.Controllers
{
    public class ApiControllerBase : ControllerBase
    {
        protected readonly IUserResolverService UserResolverService;

        public ApiControllerBase(IUserResolverService userResolverService)
        {
            this.UserResolverService = userResolverService;
        }

        protected string CurrentUserName => UserResolverService.CurrentUserName();

        protected Guid CurrentUserId => string.IsNullOrEmpty(UserResolverService.CurrentUserId())
            ? Guid.Empty
            : new Guid(UserResolverService.CurrentUserId());

        protected Guid CurrentUserInfoId => string.IsNullOrEmpty(UserResolverService.CurrentUserInfoId())
            ? Guid.Empty
            : new Guid(UserResolverService.CurrentUserInfoId());
    }
}