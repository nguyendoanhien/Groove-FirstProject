using System;

namespace GrooveMessengerAPI.Areas.Chat.Models
{
    public class UserProfile
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public string DisplayName { get; set; }
        public string Mood { get; set; }
        public string Status { get; set; }
        public string Avatar { get; set; }
    }
}