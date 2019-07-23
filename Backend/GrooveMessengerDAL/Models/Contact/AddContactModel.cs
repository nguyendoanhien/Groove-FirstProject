using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GrooveMessengerDAL.Models.Contact
{
    public class AddContactModel
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        public string ContactId { get; set; }
        [Required]
        public string NickName { get; set; }
    }
}
