using System.Threading.Tasks;
using GrooveMessengerAPI.Areas.Chat.Models;

namespace GrooveMessengerAPI.Hubs.Utils
{
    public interface IUserProfileHubClient
    {
        Task ClientChangeUserProfile(UserProfile userProfile);
    }
}