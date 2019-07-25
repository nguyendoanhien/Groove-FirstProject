﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrooveMessengerAPI.Areas.Chat.Models
{
    public class ContactChatList
    {
        public string ConvId { get; set; }

        public string ContactId { get; set; }

        public string DisplayName { get; set; }

        public string LastMessage { get; set; }

        public DateTime? LastMessageTime { get; set; }
    }
}
