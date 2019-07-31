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
        protected HubConnectionStorage ConnectionStore;
        protected string Topic;


        public HubBase(HubConnectionStorage connectionStore, string topic)
        {
            this.ConnectionStore = connectionStore;
            this.Topic = topic;
        }

        public override Task OnConnectedAsync()
        {
            var name = Context.User.Identity.Name;

            if (!ConnectionStore.GetConnections(Topic, name).Contains(Context.ConnectionId))
            {
                var conn = Context.ConnectionId;
                ConnectionStore.Add(Topic, name, conn);
            }

            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            var name = Context.User.Identity.Name;
            ConnectionStore.Remove(Topic, name, Context.ConnectionId);
            return base.OnDisconnectedAsync(exception);
        }
    }
}