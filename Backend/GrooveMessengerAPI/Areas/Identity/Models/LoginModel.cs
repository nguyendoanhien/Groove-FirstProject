using System.ComponentModel.DataAnnotations;

namespace GrooveMessengerAPI.Areas.Identity.Models
{
    public class LoginModel
    {
        [Required]
        [MinLength(6, ErrorMessage = "Username field must be at least 6 characters")]
        public string Username { get; set; }

        public string Password { get; set; }
    }
}