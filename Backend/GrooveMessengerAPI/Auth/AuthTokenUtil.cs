using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using GrooveMessengerDAL.Models;
using GrooveMessengerDAL.Models.User;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace GrooveMessengerAPI.Auth
{
    public class AuthTokenUtil
    {
        // For reference only
        public static JwtSecurityToken GetJwtToken(string userName, IConfiguration config)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWTAuthentication:SecretKey"]));

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, userName),
                new Claim("DisplayName", userName.Split('@')[0]),
                new Claim("UserName", userName),
                new Claim(JwtRegisteredClaimNames.Email, userName)
            };

            var token = new JwtSecurityToken(
                config["JWTAuthentication:Issuer"],
                config["JWTAuthentication:Audience"],
                claims,
                DateTime.Now,
                DateTime.Now.AddDays(1),
                new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            );
            return token;
        }

        public static JwtSecurityToken GetJwtToken(ApplicationUser user, IndexUserInfoModel userInfo,
            IConfiguration config)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWTAuthentication:SecretKey"]));

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim("UserName", user.UserName),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("UserInfoId", userInfo.Id.ToString() ?? ""),
                new Claim("UserId", user.Id)
            };

            var token = new JwtSecurityToken(
                config["JWTAuthentication:Issuer"],
                config["JWTAuthentication:Audience"],
                claims,
                DateTime.Now,
                DateTime.Now.AddDays(1),
                new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            );
            return token;
        }

        public static string GetJwtTokenString(string userName, IConfiguration config)
        {
            var token = GetJwtToken(userName, config);
            var result = new JwtSecurityTokenHandler().WriteToken(token);
            return result;
        }

        // Please use this method
        public static string GetJwtTokenString(ApplicationUser user, IndexUserInfoModel userInfo, IConfiguration config)
        {
            var token = GetJwtToken(user, userInfo, config);
            var result = new JwtSecurityTokenHandler().WriteToken(token);
            return result;
        }

        public static JwtSecurityToken DecodeJwt(string encodedJwt)
        {
            var stream = "[encoded jwt]";
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(stream);
            var tokenS = handler.ReadToken(encodedJwt) as JwtSecurityToken;
            return tokenS;
        }
    }
}