using GrooveMessengerAPI.Areas.Chat.Models;
using GrooveMessengerAPI.Hubs.Utils;
using GrooveMessengerDAL.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrooveMessengerAPI.Hubs
{
    public class UserProfileHub : HubBase<IUserProfileHubClient>
    {
        private IContactService _contactService;

        public UserProfileHub(HubConnectionStore<string> connectionStore,
            IContactService contactService) : base(connectionStore)
        {
            _contactService = contactService;
        }

        /// <summary>
        /// To broadcast event user profile changed to friends (existing in contact list)
        /// </summary>
        /// <param name="updateUserProfile">An user profile changed</param>
        /// <returns></returns>
        public async Task ChangeUserProfile(UserProfile updateUserProfile)
        {
            var emailList = await _contactService.GetUserContactEmailList();

            foreach (var connectionId in connectionStore.GetConnections(emailList))
            {
                await Clients.Client(connectionId).ClientChangeUserProfile(updateUserProfile);
            }
        }

        public override Task OnConnectedAsync()
        {
            // Do something just related to user profile hub
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            // Do something just related to user profile hub            
            return base.OnDisconnectedAsync(exception);
        }

    }
}
