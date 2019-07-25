﻿using GrooveMessengerAPI.Areas.Chat.Models;
using GrooveMessengerAPI.Hubs.Utils;
using GrooveMessengerDAL.Services.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrooveMessengerAPI.Hubs
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UserProfileHub : HubBase<IUserProfileHubClient>
    {
        private IContactService _contactService;

        public UserProfileHub(HubConnectionStorage connectionStore,
            IContactService contactService) : base(connectionStore)
        {
            _contactService = contactService;
            topic = "profile";
        }





        public async Task ChangeUserProfile(UserProfile updateUserProfile)
        {
            var emailList = await _contactService.GetUserContactEmailList();

            foreach (var connectionId in connectionStore.GetConnections(topic, emailList))
            {
                await Clients.Client(connectionId).ClientChangeUserProfile(updateUserProfile);
            }

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
