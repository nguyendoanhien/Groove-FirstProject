using System.ComponentModel.DataAnnotations;
using GrooveMessengerDAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace GrooveMessengerAPI.Validations
{
    public class CheckMailExistAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var userManager =
                (UserManager<ApplicationUser>) validationContext.GetService(typeof(UserManager<ApplicationUser>));
            var logger =
                (ILogger<CheckMailExistAttribute>) validationContext.GetService(
                    typeof(ILogger<CheckMailExistAttribute>));
            var email = value.ToString();
            var user = userManager.FindByEmailAsync(email).Result;
            if (user == null)
                return ValidationResult.Success;
            logger.LogError("Mail " + email + " already exists");
            return new ValidationResult("Email already exists");
        }
    }
}