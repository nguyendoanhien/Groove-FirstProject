using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GrooveMessengerAPI.Areas.Chat.Models;
using GrooveMessengerAPI.Controllers;
using GrooveMessengerAPI.Hubs;
using GrooveMessengerAPI.Hubs.Utils;
using GrooveMessengerDAL.Models;
using GrooveMessengerDAL.Models.User;
using GrooveMessengerDAL.Services.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace GrooveMessengerAPI.Areas.Chat.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ApiControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserService _userService;
        private readonly IContactService _contactService;
        private readonly IHubContext<UserProfileHub, IUserProfileHubClient> _userProfileHubContext;
        private IHubContext<UserProfileHub> _hub;

        private HubConnectionStorage _hubConnectionStore;
        public UserController(
            UserManager<ApplicationUser> userManager,
            IUserService userService,
            IContactService contactService,
            IUserResolverService userResolver,
            IHubContext<UserProfileHub, IUserProfileHubClient> userProfileHubContext,
            HubConnectionStorage hubConnectionStore
            ) : base(userResolver)
        {
            _userManager = userManager;
            _userService = userService;
            _contactService = contactService;
            _userProfileHubContext = userProfileHubContext;
            _hubConnectionStore = hubConnectionStore;
        }



        [HttpGet]
        public async Task<IndexUserInfoModel> GetUserInfo()
        {
            var user = await _userManager.FindByEmailAsync(CurrentUserName);
            var result = _userService.GetUserInfo(user.Id.ToString());
            return result;
        }

        [HttpPut]
        public async Task<EditUserInfoModel> EditUserInfoAsync(EditUserInfoModel userInfo)
        {
            //Id is String 
            //But Guid

            if (ModelState.IsValid)
            {
                _userService.EditUserInfo(userInfo);

                var userProfile = new UserProfile
                {
                    Id = userInfo.Id,
                    Avatar = userInfo.Avatar,
                    DisplayName = userInfo.DisplayName,
                    Mood = userInfo.Mood,
                    Status = userInfo.Status,
                    UserId = userInfo.UserId
                };

                var emailList = await _contactService.GetUserContactEmailList();
                foreach (var connectionId in _hubConnectionStore.GetConnections("profile", emailList))
                {
                    await _userProfileHubContext.Clients.Client(connectionId).ClientChangeUserProfile(userProfile);

                    _hub.Clients.Client("");
                }
                return userInfo;
            }

            return null;
        }
        [HttpGet("getalluserinform")]
        public async Task<IEnumerable<IndexUserInfoModel>> GetAllUserInform()
        {
            return await _userService.GetAllUserInfo();
        }
    }
}