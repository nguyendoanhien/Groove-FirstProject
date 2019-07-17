using System;
using System.Text;
using GrooveMessengerDAL.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using GrooveMessengerDAL.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace GrooveMessengerAPI
{
    public partial class Startup
    {
        public void RegisterAuth(IServiceCollection services)
        {


            services.AddAuthentication(x =>
                 {
                     x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                     x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                 })
                .AddJwtBearer(cfg =>
                {
                    cfg.RequireHttpsMetadata = false;
                    cfg.SaveToken = true;
                    cfg.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidIssuer = Configuration["JWTAuthentication:Issuer"],
                        ValidAudience = Configuration["JWTAuthentication:Audience"],
                        IssuerSigningKey =
                            new SymmetricSecurityKey(
                                Encoding.UTF8.GetBytes(Configuration["JWTAuthentication:SecretKey"])),
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero,
                        ValidateIssuer = true,
                        ValidateAudience = true
                    };

                    cfg.Events = new JwtBearerEvents()
                    {
                        OnAuthenticationFailed = context =>
                        {
                            context.NoResult();
                            context.Response.StatusCode = 401;
                            context.Response.ContentType = "text/plain";
                            context.Response.WriteAsync("Token is not valid.").Wait();
                            return Task.CompletedTask;
                        },
                        //OnChallenge = context =>
                        //{
                        //    context.Response.StatusCode = 401;
                        //    context.Response.ContentType = "text/plain";
                        //    context.Response.WriteAsync("Please provide token.").Wait();
                        //    return Task.CompletedTask;
                        //}
                    };
                }).AddGoogle(options =>
            {
                options.ClientId = Configuration.GetSection("ApplicationGoogle:ClientId").Value;
                options.ClientSecret = Configuration.GetSection("ApplicationGoogle:ClientSecret").Value; ;
            });
        }

        public void RegisterIdentity(IServiceCollection services)
        {
            services.AddDbContext<GrooveMessengerDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
                {
                    // Lockout settings
                    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                    options.Lockout.MaxFailedAccessAttempts = 5;
                    options.Lockout.AllowedForNewUsers = true;
                })
                .AddEntityFrameworkStores<GrooveMessengerDbContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = false;
                options.Password.RequiredUniqueChars = 6;

                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                options.Lockout.MaxFailedAccessAttempts = 10;
                options.Lockout.AllowedForNewUsers = true;

                // User settings
                options.User.RequireUniqueEmail = true;
            });
            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.Cookie.Expiration = TimeSpan.FromMinutes(20);
                options.LoginPath =
                    "/Identity/Account/Login"; // If the LoginPath is not set here, ASP.NET Core will default to /Account/Login
                options.LogoutPath =
                    "/Identity/Account/Logout"; // If the LogoutPath is not set here, ASP.NET Core will default to /Account/Logout
                options.AccessDeniedPath =
                    "/Identity/Account/AccessDenied"; // If the AccessDeniedPath is not set here, ASP.NET Core will default to /Account/AccessDenied
                options.SlidingExpiration = true;
                options.Cookie.Name = "GrooveMessengerAPIIdentity";
            });
        }
    }
}