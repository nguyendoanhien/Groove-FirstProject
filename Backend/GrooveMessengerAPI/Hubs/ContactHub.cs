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
    public class ContactHub : HubBase<IContactHubClient>
    {

        public ContactHub(
            HubConnectionStorage connectionStore,
            UserManager<ApplicationUser> userManager,
            IConversationService conversationService

        )
            : base(connectionStore, HubConstant.ContactHubTopic, conversationService, userManager)
        {    
        }


        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            return base.OnDisconnectedAsync(exception);
        }
    }
}