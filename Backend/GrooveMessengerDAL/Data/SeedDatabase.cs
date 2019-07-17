using GrooveMessengerDAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace GrooveMessengerDAL.Data
{
    public class SeedDatabase
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<GrooveMessengerDbContext>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            context.Database.EnsureCreated();
            if (!context.Users.Any<ApplicationUser>())
            {
                ApplicationUser user01 = new ApplicationUser()
                {
                    Email = ".",
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = "root123@gmail.com",                    
                    EmailConfirmed = true
                };
                userManager.CreateAsync(user01, "Root@123"); // admin is password
            }
        }
    }
}
