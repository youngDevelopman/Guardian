using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Guardian.AuthorizationService
{
    public class TokenManager
    {
        HMACSHA256 hmac;
        string secret;
        public TokenManager()
        {
            hmac = new HMACSHA256();
            secret = Convert.ToBase64String(hmac.Key);
        }
        public string GenerateToken(string username)
        {
            byte[] key = Convert.FromBase64String(secret);
            var securityKey = new SymmetricSecurityKey(key);

            var descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                      new Claim(ClaimTypes.Name, username)}),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(securityKey,
                  SecurityAlgorithms.HmacSha256Signature)
            };

            var handler = new JwtSecurityTokenHandler();

            JwtSecurityToken token = handler.CreateJwtSecurityToken(descriptor);


            return handler.WriteToken(token);
        }
    }
}
