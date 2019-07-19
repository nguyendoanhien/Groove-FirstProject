using GrooveMessengerAPI.Hubs.Utils;
using GrooveMessengerDAL.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrooveMessengerAPI.Hubs
{
    public class UserProfileHub: HubBase<IUserProfileHubClient>
    {
        private IContactService _contactService;

        public UserProfileHub(
            HubConnectionStore<string> connectionStore,
            IContactService contactService) : base(connectionStore)
        {
            _contactService = contactService;
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
