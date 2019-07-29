using System;

namespace GrooveMessengerAPI.Areas.Chat.Models
{
    public class Message
    {
        public Message(Guid FromConv, string FromSender, Guid MessageId, string Payload, DateTime Time)
        {
            this.FromConv = FromConv;
            this.FromSender = FromSender;
            this.MessageId = MessageId;
            this.Payload = Payload;
            this.Time = Time;
        }

        public Guid FromConv { get; set; } // Conv Id
        public string FromSender { get; set; } // Identity User Id
        public Guid MessageId { get; set; }
        public string Payload { get; set; }
        public DateTime Time { get; set; }
    }
}