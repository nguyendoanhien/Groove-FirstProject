using GrooveMessengerAPI.Areas.Chat.Models;
using GrooveMessengerAPI.Hubs.Utils;
using GrooveMessengerDAL.Services.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GrooveMessengerAPI.Hubs
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class MessageHub : Hub<ITypedHubClient>
    {
        private readonly static ConnectionMapping<string> _connections =
           new ConnectionMapping<string>();
        private IContactService _contactService;
        
        public MessageHub(IContactService contactService)
        {
            _contactService = contactService;
        }
        public override Task OnConnectedAsync()
        {
            string name = Context.User.Identity.Name;

            if (!_connections.GetConnections(name).Contains(Context.ConnectionId))
            {
                var conn = Context.ConnectionId;
                _connections.Add(name, conn);
            }
            return base.OnConnectedAsync();
        }

        public async Task SendMessageToUser(Message message, string toUser)
        {
            string username = Context.User.Identity.Name;
            message.From = username;
            foreach (var connectionId in _connections.GetConnections(toUser))
            {
                await Clients.Client(connectionId).SendMessage(message);
            }
        }

        public async Task SendRemovedMessageToUser(Message message, string toUser)
        {
            string username = Context.User.Identity.Name;
            message.From = username;
            foreach (var connectionId in _connections.GetConnections(toUser))
            {
                await Clients.Client(connectionId).SendRemovedMessage(message);
            }
        }

        public async Task SendEditedMessageToUser(Message message, string toUser)
        {
            string username = Context.User.Identity.Name;
            message.From = username;
            foreach (var connectionId in _connections.GetConnections(toUser))
            {
                await Clients.Client(connectionId).SendEditedMessage(message);
            }
        }

        public async Task SendMessageViewingStatus(string toUser)
        {
            string username = Context.User.Identity.Name;

            foreach (var connectionId in _connections.GetConnections(toUser))
            {
                await Clients.Client(connectionId).SendMessageViewingStatus(username);
            }
        }
        public override Task OnDisconnectedAsync(Exception exception)
        {
            string name = Context.User.Identity.Name;

            _connections.Remove(name, Context.ConnectionId);

            return base.OnDisconnectedAsync(exception);
        }
    }
}
