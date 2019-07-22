using GrooveMessengerAPI.Areas.Chat.Models;
using GrooveMessengerAPI.Hubs.Utils;
using GrooveMessengerDAL.Models;
using GrooveMessengerDAL.Services.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace GrooveMessengerAPI.Hubs
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class MessageHub : HubBase<IMessageHubClient>
    {
        private IContactService _contactService;
        private UserManager<ApplicationUser> _userManager;
        public MessageHub(
            HubConnectionStore<string> connectionStore,
            UserManager<ApplicationUser> userManager,
            IContactService contactService):base(connectionStore)
        {
            _userManager = userManager;
            _contactService = contactService;
        }
        

        public async Task SendMessageToUser(Message message, string toUser)
        {
            string username = Context.User.Identity.Name;            
            var recieverInform = await _userManager.FindByIdAsync(toUser);
            foreach (var connectionId in connectionStore.GetConnections(recieverInform.UserName))
            {
                await Clients.Client(connectionId).SendMessage(message);
            }
        }

        //public async Task SendRemovedMessageToUser(Message message, string toUser)
        //{
        //    string username = Context.User.Identity.Name;
        //    message.From = username;
        //    foreach (var connectionId in connectionStore.GetConnections(toUser))
        //    {
        //        await Clients.Client(connectionId).SendRemovedMessage(message);
        //    }
        //}

        //public async Task SendEditedMessageToUser(Message message, string toUser)
        //{
        //    string username = Context.User.Identity.Name;
        //    message.From = username;
        //    foreach (var connectionId in connectionStore.GetConnections(toUser))
        //    {
        //        await Clients.Client(connectionId).SendEditedMessage(message);
        //    }
        //}

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
