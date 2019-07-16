using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GrooveMessengerDAL.Entities
{
    public class UserInfoContactEntity:AuditBaseEntity<Guid>
    {

        public  Guid UserId { get; set; }

        public  Guid ContactId { get; set; }

        public string NickName { get; set; }

        public virtual UserInfoEntity UserInfo { get; set; }

        public virtual UserInfoEntity ContactInfo { get; set; }
    }
}
