using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GrooveMessengerDAL.Entities
{
    public class UserInfoContactEntity: AuditBaseEntity<Guid>
    {
        public string UserId { get; set; }

        public string ContactId { get; set; }

        public string NickName { get; set; }

        public UserInfoEntity UserInfo { get; set; }

        public UserInfoEntity ContactInfo { get; set; }
    }
}
