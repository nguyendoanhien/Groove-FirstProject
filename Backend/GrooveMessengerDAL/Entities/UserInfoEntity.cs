using System;
using System.Collections.Generic;
using System.ComponentModel;
using GrooveMessengerDAL.Models;

namespace GrooveMessengerDAL.Entities
{
    public class UserInfoEntity : AuditBaseEntity<Guid>
    {
        public enum StatusName
        {
            online,
            away,
            [Description("do-not-disturb")] doNotDisturb,
            offline
        }

        public string DisplayName { get; set; }


        public string Mood { get; set; }

        public StatusName Status { get; set; }

        public string Avatar { get; set; }

        public string UserId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        public virtual ICollection<UserInfoContactEntity> Users { get; set; }
        public virtual ICollection<UserInfoContactEntity> Contacts { get; set; }
    }
}