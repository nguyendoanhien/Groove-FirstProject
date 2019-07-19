using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GrooveMessengerAPI.Controllers;
using GrooveMessengerDAL.Models;
using GrooveMessengerDAL.Models.User;
using GrooveMessengerDAL.Services.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GrooveMessengerAPI.Areas.Chat.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ApiControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserService _userService;
        public UserController(
            UserManager<ApplicationUser> userManager,
            IUserService userService,
            IUserResolverService userResolver
            ) : base(userResolver)
        {
            _userManager = userManager;
            _userService = userService;
        }



        [HttpGet]
        public async Task<IndexUserInfoModel> GetUserInfo()
        {
            var user = await _userManager.FindByEmailAsync(CurrentUserName);
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