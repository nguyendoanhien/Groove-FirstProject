using System;
using System.Linq;
using System.Threading.Tasks;
using GrooveMessengerAPI.Hubs.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace GrooveMessengerAPI.Hubs
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class HubBase<T> : Hub<T> where T : class
    {
        protected HubConnectionStorage connectionStore;
        protected string topic;


        public HubBase(HubConnectionStorage connectionStore)
        {
            this.connectionStore = connectionStore;
        }

        public override Task OnConnectedAsync()
        {
            var name = Context.User.Identity.Name;

            if (!connectionStore.GetConnections(topic, name).Contains(Context.ConnectionId))
            {
                var conn = Context.ConnectionId;
                connectionStore.Add(topic, name, conn);
            }

            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            var name = Context.User.Identity.Name;
            connectionStore.Remove(topic, name, Context.ConnectionId);
            return base.OnDisconnectedAsync(exception);
        }
    }
}