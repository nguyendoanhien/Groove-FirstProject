using System;
using System.Threading.Tasks;
using GrooveMessengerAPI.Constants;
using GrooveMessengerAPI.Hubs.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace GrooveMessengerAPI.Hubs
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class MessageHub : HubBase<IMessageHubClient>
    {
        public MessageHub(
            HubConnectionStorage connectionStore
        ) : base(connectionStore, HubConstant.MessageHubTopic)
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