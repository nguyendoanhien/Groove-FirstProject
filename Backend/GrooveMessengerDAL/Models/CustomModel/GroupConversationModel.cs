using System;
using System.Collections.Generic;
using System.Text;

namespace GrooveMessengerDAL.Models.CustomModel
{
    public class GroupConversationModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Avatar { get; set; }

        public string LastestMessage { get; set; }

        public DateTime LastestMessageTime { get; set; }

        public int UnreadMessage { get; set; }

        public List<MemberInformModel> Members { get; set; }
    }
}
