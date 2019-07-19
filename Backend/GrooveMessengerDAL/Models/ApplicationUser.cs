using GrooveMessengerDAL.Entities;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GrooveMessengerDAL.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public virtual UserInfoEntity UserInfoEntity { get; set; }

        public ICollection<MessageEntity> MessageEntity { get; set; }

        public virtual ICollection<ParticipantEntity> ParticipantEntity { get; set; }
    }

}
