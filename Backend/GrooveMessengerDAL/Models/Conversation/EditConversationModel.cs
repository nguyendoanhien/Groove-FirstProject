using GrooveMessengerDAL.Models.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace GrooveMessengerDAL.Models.Conversation
{
    public class EditConversationModel
    {
        public Guid Id { get; set; }
        public string Avatar { get; set; }
        public string Name { get; set; }
        public IEnumerable<IndexUserInfoModel> Members { get; set; }
    }
}
