using System;

namespace GrooveMessengerDAL.Models.CustomModel
{
    public class ContactLatestChatListModel
    {
        public Guid ConvId { get; set; }

        public Guid ContactId { get; set; }

        public string DisplayName { get; set; }

        public string LastMessage { get; set; }

        public DateTime? LastMessageTime { get; set; }

    }
    [AttributeUsage(AttributeTargets.Property, Inherited = false)]
    public class MapBy : Attribute
    {
        public MapBy(Type enumType)
        {

        }
    }
}
