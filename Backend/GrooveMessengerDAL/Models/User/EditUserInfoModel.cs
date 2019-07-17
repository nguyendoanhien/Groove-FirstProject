using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GrooveMessengerDAL.Models.User
{
    public class EditUserInfoModel:AuditBaseModel<Guid>
    {
        public string UserId { get; set; }

        [MaxLength(120), Required]
        public string DisplayName { get; set; }

        [MaxLength(150)]
        public string Mood { get; set; }

        [Required]
        public string Status { get; set; }

        public string Avatar { get; set; }
    }
}
