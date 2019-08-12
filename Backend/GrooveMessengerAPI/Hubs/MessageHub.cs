using System;
using System.Threading.Tasks;
using GrooveMessengerAPI.Constants;
using GrooveMessengerAPI.Hubs.Utils;
using GrooveMessengerDAL.Models;
using GrooveMessengerDAL.Services.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace GrooveMessengerAPI.Hubs
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class MessageHub : HubBase<IMessageHubClient>
    {
        public MessageHub(
            HubConnectionStorage connectionStore,
            IConversationService conversationService,
            UserManager<ApplicationUser> userManager
        ) : base(connectionStore, HubConstant.MessageHubTopic, conversationService, userManager)
        {        
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