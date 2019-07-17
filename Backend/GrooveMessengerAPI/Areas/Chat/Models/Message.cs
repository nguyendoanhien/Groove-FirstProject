using System;

namespace GrooveMessengerAPI.Areas.Chat.Models
{
    public class Message
    {
        public string From { get; set; }
        public Guid Id { get; set; }
        public string Payload { get; set; }
    }
}
