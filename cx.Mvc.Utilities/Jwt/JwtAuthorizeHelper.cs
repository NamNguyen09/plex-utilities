using System;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace cx.Mvc.Utilities.Jwt
{
    public static class JwtAuthorizeHelper
    {
        public static ClaimsPrincipal ValidateJwtToken(this string token, string securityKey, out SecurityToken securityToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.Default.GetBytes(securityKey.PadRight(512 / 8, '\0'));
            SymmetricSecurityKey signingKey = new SymmetricSecurityKey(key);
            return tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ////ValidateIssuerSigningKey = true,
                ValidateLifetime = true,
                IssuerSigningKey = signingKey,
                ValidateIssuer = false,
                ValidateAudience = false,
                // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                ClockSkew = TimeSpan.Zero,
                LifetimeValidator = CustomLifetimeValidator
            }, out securityToken);
        }
        private static bool CustomLifetimeValidator(DateTime? notBefore, DateTime? expires, SecurityToken securityToken, TokenValidationParameters validationParameters)
        {
            if (expires != null)
            {
                double daysToExpired = Convert.ToDouble(ConfigurationManager.AppSettings["JwtDaysToExpiredToken"] ?? "0");
                return expires.Value.AddDays(daysToExpired) > DateTime.UtcNow;
            }
            return false;
        }
    }
}
