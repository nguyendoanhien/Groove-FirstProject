using System;
using System.Collections.Generic;
using System.Text;

namespace GrooveMessengerDAL.Models.Contact
{
    public class FullContactModel
    {
        public Guid UserId { get; set; }
        public Guid ContactId { get; set; }
        public string NickName { get; set; }
        public bool? Deleted { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
    }
}
