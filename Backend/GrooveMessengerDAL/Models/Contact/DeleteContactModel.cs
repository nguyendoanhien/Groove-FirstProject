using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GrooveMessengerDAL.Models.Contact
{
    public class DeleteContactModel
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        public string ContactId { get; set; }
        //public string NickName { get; set; }
    }
}
