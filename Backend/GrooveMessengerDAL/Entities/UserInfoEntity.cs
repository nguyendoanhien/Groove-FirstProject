using GrooveMessengerDAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GrooveMessengerDAL.Entities
{
    public class UserInfoEntity: AuditBaseEntity<string>
    {
        public enum StatusName
        {
            Online,
            Away,
            [Description("Do not disturb")]
            DoNotDisturb,
            Offline
        }

        [MaxLength(120), Required]
        public string DisplayName { get; set; }

        [MaxLength(150)]
        public string Mood { get; set; }

        [Required]
        public StatusName Status { get; set; }

        public string Avatar { get; set; }

        public ApplicationUser ApplicationUser { get; set; }
    }
}
