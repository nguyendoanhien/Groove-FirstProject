using System;

namespace GrooveMessengerAPI.Areas.Chat.Models
{
    public class Message
    {
        public string From { get; set; }
        public string Id { get; set; }
        public string Payload { get; set; }
        public Message(string From, string Id, string Payload)
        {
            this.From = From;
            this.Id = Id;
            this.Payload = Payload;
        }
    }
}
