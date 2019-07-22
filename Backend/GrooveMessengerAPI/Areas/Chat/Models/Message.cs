using System;

namespace GrooveMessengerAPI.Areas.Chat.Models
{
    public class Message
    {
        public string FromConv { get; set; } // Conv Id
        public string FromSender { get; set; } // Identity User Id
        public string MessageId { get; set; }
        public string Payload { get; set; }
        public DateTime Time { get; set; }
        public Message(string FromConv, string FromSender, string MessageId, string Payload, DateTime Time)
        {
            this.FromConv = FromConv;
            this.FromSender = FromSender;
            this.MessageId = MessageId;
            this.Payload = Payload;
            this.Time = Time;
        }
    }
}
