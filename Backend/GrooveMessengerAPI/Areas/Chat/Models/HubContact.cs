using GrooveMessengerDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrooveMessengerAPI.Areas.Chat.Models.Contact
{
    public class HubContact
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public string DisplayName { get; set; }
        public string Mood { get; set; }
        [MapBy(typeof(StatusName))]
        public string Status { get; set; }
        public string Avatar { get; set; }
    }
}
