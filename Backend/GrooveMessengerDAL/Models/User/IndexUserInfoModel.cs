using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GrooveMessengerDAL.Models.User
{
    public class IndexUserInfoModel
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public string DisplayName { get; set; }
        public string Mood { get; set; }
        public string Status { get; set; }
        public string Avatar { get; set; }
    }
}
