using GrooveMessengerAPI.Areas.Chat.Models;
using GrooveMessengerAPI.Hubs.Utils;
using GrooveMessengerDAL.Services.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Threading.Tasks;

namespace GrooveMessengerAPI.Hubs
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class MessageHub : HubBase<IMessageHubClient>
    {
        private IContactService _contactService;
        
        public MessageHub(
            HubConnectionStore<string> connectionStore,
            IContactService contactService):base(connectionStore)
        {
            _contactService = contactService;
        }
        

        public async Task SendMessageToUser(string message, string toUser)
        {
            string username = Context.User.Identity.Name;
            Message chatMessage = new Message(username, "aa-aa-aa-aa" , message);
            foreach (var connectionId in connectionStore.GetConnections(toUser))
            {
                await Clients.Client(connectionId).SendMessage(chatMessage);
            }
        }

        public async Task SendRemovedMessageToUser(Message message, string toUser)
        {
            string username = Context.User.Identity.Name;
            message.From = username;
            foreach (var connectionId in connectionStore.GetConnections(toUser))
            {
                await Clients.Client(connectionId).SendRemovedMessage(message);
            }
        }

        public async Task SendEditedMessageToUser(Message message, string toUser)
        {
            string username = Context.User.Identity.Name;
            message.From = username;
            foreach (var connectionId in connectionStore.GetConnections(toUser))
            {
                await Clients.Client(connectionId).SendEditedMessage(message);
            }
        }

        public async Task SendMessageViewingStatus(string toUser)
        {
            string username = Context.User.Identity.Name;

            foreach (var connectionId in connectionStore.GetConnections(toUser))
            {
                await Clients.Client(connectionId).SendMessageViewingStatus(username);
            }
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
