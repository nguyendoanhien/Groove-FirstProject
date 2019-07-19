using GrooveMessengerAPI.Areas.Chat.Models;
using GrooveMessengerAPI.Areas.Chat.Models.Contact;
using GrooveMessengerAPI.Hubs.Utils;
using GrooveMessengerDAL.Services.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Threading.Tasks;

namespace GrooveMessengerAPI.Hubs
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ContactHub : HubBase<IContactHubClient>
    {
        private IContactService _contactService;

        public ContactHub(
            HubConnectionStore<string> connectionStore,
            IContactService contactService) : base(connectionStore)
        {
            _contactService = contactService;
        }


        public async Task SendNewContactToUser(HubContact fromUserContact, string toUser)
        {
            // Do something if needed
            foreach (var connectionId in connectionStore.GetConnections(toUser))
            {
                await Clients.Client(connectionId).SendNewContactToFriend(fromUserContact);
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
