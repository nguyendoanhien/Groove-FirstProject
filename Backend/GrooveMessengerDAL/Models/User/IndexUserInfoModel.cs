using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using static GrooveMessengerDAL.Entities.UserInfoEntity;

namespace GrooveMessengerDAL.Models.User
{
    public class IndexUserInfoModel
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public string DisplayName { get; set; }        
        public string Mood { get; set; }
        [MapBy(typeof(StatusName))]
        public string Status { get; set; }
        public string Avatar { get; set; }
    }    
}
