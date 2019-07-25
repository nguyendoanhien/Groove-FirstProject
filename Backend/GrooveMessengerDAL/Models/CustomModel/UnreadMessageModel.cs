using System;
using System.Collections.Generic;
using System.Text;

namespace GrooveMessengerDAL.Models.CustomModel
{
    public class UnreadMessageModel
    {
        public Guid ConversationId { get; set; }
        public int Amount { get; set; }
    }
}
