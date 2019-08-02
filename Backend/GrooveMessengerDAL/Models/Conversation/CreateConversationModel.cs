using System;

namespace GrooveMessengerDAL.Models.Conversation
{
    public class CreateConversationModel
    {
        public Guid Id { get; set; }
        public string Avatar { get; set; }
        public string Name { get; set; }
        public bool IsGroup { get; set; }
    }
}