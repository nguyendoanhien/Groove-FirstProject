using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrooveMessengerAPI.Areas.Chat.Models.Contact
{
    public class HubContact
    {
        #region User who wants to make friend
        //email
        public string FromUser { get; set; }
        public string DisplayName { get; set; }
        public string Mood { get; set; }
        public string Status { get; set; }
        public string Avatar { get; set; }
        #endregion


        //email
        public string ToUser { get; set; }
        public string connId { get; set; }


        public HubContact(string ToUser,string id)
        {
            this.ToUser = ToUser;
            this.connId = id;
        }
    }
}
