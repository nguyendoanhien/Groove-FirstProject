using GrooveMessengerAPI.Areas.Chat.Models;
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


        public UserProfileHub(HubConnectionStore<string> connectionStore) : base(connectionStore)
        {
  
        }


        public async  Task ChangeUserProfile(UserProfile userProfile)
        {
            await Clients.All.UserProfile(userProfile);

        }

        public override Task OnConnectedAsync()
        {
            // Do something just related to message hub
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            // Do something just related to message hub            
            return base.OnDisconnectedAsync(exception);
        }

    }
}
