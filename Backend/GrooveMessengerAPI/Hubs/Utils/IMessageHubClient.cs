using GrooveMessengerAPI.Areas.Chat.Models;
using System.Threading.Tasks;

namespace GrooveMessengerAPI.Hubs.Utils
{
    public interface IMessageHubClient
    {
        Task SendMessage(Message message);
        Task SendRemovedMessage(Message message);
        Task SendEditedMessage(Message message);
        Task SendMessageViewingStatus(string fromUser);
    }

    public enum MessageEventTypes
    {
        Broadcast = 0,
        Individual = 1,
        Group = 2
    }
}
