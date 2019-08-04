using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrooveMessengerAPI.Areas.Chat.Models
{
    public class MessageInGroup
    {
        public MessageInGroup(Guid FromConv, string FromSender, Guid MessageId, string SenderName, string SenderAvatar, string Payload, DateTime Time)
        {
            this.FromConv = FromConv;
            this.FromSender = FromSender;
            this.MessageId = MessageId;
            this.Payload = Payload;
            this.Time = Time;
            this.SenderName = SenderName;
            this.SenderAvatar = SenderAvatar;
        }

        public Guid FromConv { get; set; } // Conv Id
        public string FromSender { get; set; } // Identity User Id
        public string SenderName { get; set; }
        public string SenderAvatar { get; set; }
        public Guid MessageId { get; set; }
        public string Payload { get; set; }
        public DateTime Time { get; set; }
    }
}
