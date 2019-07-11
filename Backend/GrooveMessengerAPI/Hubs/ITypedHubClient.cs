using GrooveMessengerAPI.Areas.Chat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrooveMessengerAPI.Hubs
{
    public interface ITypedHubClient
    {
        Task BroadcastMessage(Message message);
        Task SendMessage(Message message);
        Task ReceiveMessage(Message message);

    }

    public enum MessageEventTypes
    {
        Broadcast = 0,
        Individual = 1,
        Group = 2
    }
}
