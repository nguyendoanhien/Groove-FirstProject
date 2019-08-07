using System;

namespace GrooveMessengerAPI.Areas.Chat.Models
{
    public class Message
    {
        public Message(Guid fromConv, string fromSender, Guid messageId, string payload, DateTime time, string type)
        {
            this.FromConv = fromConv;
            this.FromSender = fromSender;
            this.MessageId = messageId;
            this.Payload = payload;
            this.Time = time;
            this.Type = type;
        }

        public Guid FromConv { get; set; } // Conv Id
        public string FromSender { get; set; } // Identity User Id
        public Guid MessageId { get; set; }
        public string Payload { get; set; }
        public DateTime Time { get; set; }
        public string Type { get; set; }
    }
}