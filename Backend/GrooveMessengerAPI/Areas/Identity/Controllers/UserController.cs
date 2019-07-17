using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GrooveMessengerDAL.Models;
using GrooveMessengerDAL.Models.User;
using GrooveMessengerDAL.Services.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GrooveMessengerAPI.Areas.Identity.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("Identity/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserService _userService;
        private readonly IUserResolverService _userResolver;
        public UserController(
            UserManager<ApplicationUser> userManager,
            IUserService userService,
            IUserResolverService userResolver
            )
        {
            _userManager = userManager;
            _userService = userService;
            _userResolver = userResolver;
        }



        [HttpGet]
        public async Task<IndexUserInfoModel> GetUserInfo()
        {
            var email = _userResolver.CurrentUserName();
            var user = await  _userManager.FindByEmailAsync(email);
            var result = _userService.GetUserInfo(user.Id.ToString());
            return result;
        }

        [HttpPut]
        public EditUserInfoModel EditUserInfo(EditUserInfoModel userInfo)
        {
            //Id is String 
            //But Guid

            if (ModelState.IsValid)
            {
                _userService.EditUserInfo(userInfo);
                return userInfo;
            }

            return null;
        }
    }
}