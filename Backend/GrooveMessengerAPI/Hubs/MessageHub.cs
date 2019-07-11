using GrooveMessengerAPI.Areas.Chat.Models;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrooveMessengerAPI.Hubs
{
    public class MessageHub : Hub<ITypedHubClient>
    {
        public static Dictionary<string, string> connections = new Dictionary<string, string>();

        public void Join(string userName)
        {
            connections.Add(userName, Context.ConnectionId);
        }

        public async Task BroadcastMessage(Message message)
        {
            await Clients.All.BroadcastMessage(message);
        }

        public async Task SendMessageToUser(Message message, string toUser)
        {
            var connectionId = connections[toUser];
            await Clients.Client(connectionId).ReceiveMessage(message);

        }

        public async Task SendMessageToGroup(Message message, string groupName)
        {
            await Clients.OthersInGroup(groupName).BroadcastMessage(message);
        }

        public async Task JoinGroup(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        }

        public async Task LeaveGroup(string roomName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomName);
        }
    }
}
