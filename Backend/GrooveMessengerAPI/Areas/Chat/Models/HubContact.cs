using System;
using GrooveMessengerDAL.Models;

namespace GrooveMessengerAPI.Areas.Chat.Models.Contact
{
    public class HubContact
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public string DisplayName { get; set; }
        public string Mood { get; set; }

        [MapBy(typeof(StatusName))] public string Status { get; set; }

        public string Avatar { get; set; }
    }
}