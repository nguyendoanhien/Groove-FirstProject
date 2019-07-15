using GrooveMessengerDAL.Entities;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace GrooveMessengerDAL.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        [StringLength(50, ErrorMessage = "50 characters maximum")]
        public string DisplayName { get; set; }


        public UserInfoEntity UserInfoEntity { get; set; }
    }

}
