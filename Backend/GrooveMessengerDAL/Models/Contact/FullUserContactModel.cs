using System;

namespace GrooveMessengerDAL.Models.Contact
{
    public class FullContactModel
    {
        //UserInfoContact
        public Guid UserId { get; set; }
        public Guid ContactId { get; set; }
        public string NickName { get; set; }
        public bool? Deleted { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
    }
}