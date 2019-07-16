using GrooveMessengerAPI.Validations;
using System.ComponentModel.DataAnnotations;

namespace GrooveMessengerAPI.Areas.Identity.Models
{
    public class RegisterModel
    {        
        [MinLength(6,ErrorMessage ="Display name must be 6 charactor")]
        public string DisplayName { get; set; }
        [CheckMailExist]
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
