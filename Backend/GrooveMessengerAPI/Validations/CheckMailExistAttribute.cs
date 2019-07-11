using GrooveMessengerDAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

namespace GrooveMessengerAPI.Validations
{
    public class CheckMailExistAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            UserManager<ApplicationUser>  _userManager = (UserManager<ApplicationUser>)validationContext.GetService(typeof(UserManager<ApplicationUser>));
            ILogger<CheckMailExistAttribute> _logger = (ILogger<CheckMailExistAttribute>)validationContext.GetService(typeof(ILogger<CheckMailExistAttribute>));
            string email = value.ToString();
            var user = _userManager.FindByEmailAsync(email).Result;          
            if (user == null)
                return ValidationResult.Success;
            _logger.LogError("Mail " + email + " already exists");
            return new ValidationResult("Email already exists");
           
        }
    }
}
