using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrooveMessengerAPI.Areas.Chat.Models
{
    public class Message
    {
        public string From { get; set; }
        public string Payload { get; set; }
    }
}
