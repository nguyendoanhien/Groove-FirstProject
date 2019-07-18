using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace GrooveMessengerAPI.Auth
{
    public class AuthTokenUtil
    {
        public static JwtSecurityToken GetJwtToken(string userName, IConfiguration config)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWTAuthentication:SecretKey"]));

            var claims = new Claim[]
            {
                new Claim(ClaimTypes.Name, userName),
                new Claim("DisplayName", userName.Split('@')[0]),
                new Claim("UserName", userName),
                new Claim(JwtRegisteredClaimNames.Email, userName)
            };

            var token = new JwtSecurityToken(
                issuer: config["JWTAuthentication:Issuer"],
                audience: config["JWTAuthentication:Audience"],
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            );
            return token;
        }

        public static string GetJwtTokenString(string userName, IConfiguration config)
        {
            var token = GetJwtToken(userName, config);
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