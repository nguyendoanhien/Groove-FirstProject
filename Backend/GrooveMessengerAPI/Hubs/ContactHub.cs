using GrooveMessengerAPI.Areas.Chat.Models;
using GrooveMessengerAPI.Areas.Chat.Models.Contact;
using GrooveMessengerAPI.Hubs.Utils;
using GrooveMessengerDAL.Services.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Linq;
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
            string username = Context.User.Identity.Name;
            fromUserContact.ToUser = toUser;
            if (!connectionStore.GetConnections(toUser).Contains(Context.ConnectionId))
            {
                var conn = Context.ConnectionId;
                connectionStore.Add(toUser, conn);
            }


            
                
            

   
                

            
            foreach (var connectionId in connectionStore.GetConnections(toUser))
            {
                await Clients.Client(connectionId).SendNewContactToFriend(fromUserContact);
            }
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
