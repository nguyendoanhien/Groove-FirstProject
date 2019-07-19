using GrooveMessengerAPI.Hubs.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GrooveMessengerAPI.Hubs
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class HubBase<T> : Hub<T> where T:class
    {
        protected HubConnectionStore<string> connectionStore;

        public HubBase(HubConnectionStore<string> connectionStore)
        {
            this.connectionStore = connectionStore;
        }

        public override Task OnConnectedAsync()
        {
            string name = Context.User.Identity.Name;

            if (!connectionStore.GetConnections(name).Contains(Context.ConnectionId))
            {
                var conn = Context.ConnectionId;
                connectionStore.Add(name, conn);
            }
            return base.OnConnectedAsync();
        }
        
        public override Task OnDisconnectedAsync(Exception exception)
        {
            string name = Context.User.Identity.Name;
            connectionStore.Remove(name, Context.ConnectionId);
            return base.OnDisconnectedAsync(exception);
        }
    }
}
