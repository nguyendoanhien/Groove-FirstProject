﻿using System;
using System.Threading.Tasks;
using GrooveMessengerAPI.Areas.Chat.Models;
using GrooveMessengerAPI.Constants;
using GrooveMessengerAPI.Hubs.Utils;
using GrooveMessengerDAL.Services.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace GrooveMessengerAPI.Hubs
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UserProfileHub : HubBase<IUserProfileHubClient>
    {
        private readonly IContactService _contactService;

        public UserProfileHub(HubConnectionStorage connectionStore,
            IContactService contactService) : base(connectionStore, HubConstant.ProfileHubTopic)
        {
            _contactService = contactService;          
        }


        public async Task ChangeUserProfile(UserProfile updateUserProfile)
        {
            var emailList = await _contactService.GetUserContactEmailList();

            foreach (var connectionId in ConnectionStore.GetConnections(Topic, emailList))
                await Clients.Client(connectionId).ClientChangeUserProfile(updateUserProfile);
        }

        public override Task OnConnectedAsync()
        {
            // Do something just related to user profile hub
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            // Do something just related to user profile hub            
            return base.OnDisconnectedAsync(exception);
        }
    }
}