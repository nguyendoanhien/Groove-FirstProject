﻿using System.ComponentModel.DataAnnotations;
using GrooveMessengerDAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace GrooveMessengerAPI.Validations
{
    public class CheckMailExistAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var _userManager =
                (UserManager<ApplicationUser>) validationContext.GetService(typeof(UserManager<ApplicationUser>));
            var _logger =
                (ILogger<CheckMailExistAttribute>) validationContext.GetService(
                    typeof(ILogger<CheckMailExistAttribute>));
            var email = value.ToString();
            var user = _userManager.FindByEmailAsync(email).Result;
            if (user == null)
                return ValidationResult.Success;
            _logger.LogError("Mail " + email + " already exists");
            return new ValidationResult("Email already exists");
        }
    }
}