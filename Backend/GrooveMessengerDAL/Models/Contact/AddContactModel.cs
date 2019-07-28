using System;
using System.ComponentModel.DataAnnotations;

namespace GrooveMessengerDAL.Models.Contact
{
    public class AddContactModel
    {
        [Required] public string UserId { get; set; }

        [Required] public Guid ContactId { get; set; }

        [Required] public string NickName { get; set; }
    }
}