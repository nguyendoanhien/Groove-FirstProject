using System;

namespace GrooveMessengerDAL.Models.CustomModel
{
    public class ContactLatestChatListModel
    {
        public Guid ConvId { get; set; }

        public string ContactId { get; set; }

        public string DisplayName { get; set; }

        public string Unread { get; set; }

        public string LastMessage { get; set; }

        public DateTime? LastMessageTime { get; set; }
    }
}