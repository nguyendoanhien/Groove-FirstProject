using GrooveMessengerDAL.Entities;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace GrooveMessengerDAL.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public UserInfoEntity UserInfoEntity { get; set; }
    }

}
