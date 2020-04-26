using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Guardian.AuthorizationService
{
    public class TokenManager
    {
        private const string USER_ID_CLAIM = "User_Id";
        private const string USER_POOL_ID_CLAIM = "User_PoolId";

        private readonly HMACSHA256 hmac;
        private readonly string secret;

        public TokenManager()
        {
            hmac = new HMACSHA256();
            secret = Convert.ToBase64String(hmac.Key);
        }

        public OAuthTokenModel GenerateToken(string userId, string userPoolId)
        {
            byte[] key = Convert.FromBase64String(secret);
            var securityKey = new SymmetricSecurityKey(key);

            var descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                      new Claim(USER_ID_CLAIM, userId), new Claim(USER_POOL_ID_CLAIM, userPoolId)}),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(securityKey,
                  SecurityAlgorithms.HmacSha256Signature)
            };

            var handler = new JwtSecurityTokenHandler();
            JwtSecurityToken tokenDescriptor = handler.CreateJwtSecurityToken(descriptor);

            string accessToken = handler.WriteToken(tokenDescriptor);
            var authTokenResponse = new OAuthTokenModel()
            {
                AccessToken = accessToken,
                ExpiresIn = descriptor.Expires.Value,
                RefreshToken = "TEMP",
            };

            return authTokenResponse;
        }

        public bool ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Convert.FromBase64String(secret);
            var mySecurityKey = new SymmetricSecurityKey(key);
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ValidateActor = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = mySecurityKey,
                }, out SecurityToken validatedToken);
            }
            catch(Exception ex)
            {
                return false;
            }
            return true;
        }
    }
}
