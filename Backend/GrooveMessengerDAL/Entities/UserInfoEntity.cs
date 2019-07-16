﻿using GrooveMessengerDAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GrooveMessengerDAL.Entities
{
    public class UserInfoEntity: AuditBaseEntity<Guid>
    {
        public enum StatusName
        {
            Online,
            Away,
            [Description("Do not disturb")]
            DoNotDisturb,
            Offline
        }
        
        public string DisplayName { get; set; }

        public string Mood { get; set; }

        public StatusName Status { get; set; }

        public string Avatar { get; set; }
        
        public String UserId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }
    }
}
