using GrooveMessengerAPI.Areas.Chat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrooveMessengerAPI.Hubs.Utils
{
    public interface IUserProfileHubClient
    {
        Task UserProfile(UserProfile userProfile);
    }
}
