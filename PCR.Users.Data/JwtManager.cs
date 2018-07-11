using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.UI.WebControls;
using Microsoft.IdentityModel.Tokens;

namespace PCR.Users.Data
{
    public static class JwtManager
    {
        public const string Secret = "856FECBA3B06519C8DDDBC80BB080553"; // your symetric

        public static string GenerateToken(PCR.Users.Model.ViewModels.UserViewModel userDetails, string databaseId, int expireMinutes = 90)
        {
            try
            {
                var symmetricKey = Convert.FromBase64String(Secret);
                var tokenHandler = new JwtSecurityTokenHandler();

                var now = DateTime.UtcNow;
                string phone = userDetails.PhoneNumber == null ? "" : userDetails.PhoneNumber;

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                            {
                        new Claim(ClaimTypes.Name, userDetails.UserName),
                        new Claim(ClaimTypes.Role, userDetails.RoleID.ToString()),
                        new Claim(ClaimTypes.Email, userDetails.EmailAddress),
                        new Claim(ClaimTypes.MobilePhone, phone),
                        new Claim(ClaimTypes.Sid, userDetails.UserID.ToString()),
                        new Claim(ClaimTypes.Authentication, databaseId)
                    }),

                    Expires = now.AddMinutes(Convert.ToInt32(expireMinutes)),

                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(symmetricKey), SecurityAlgorithms.HmacSha256Signature)
                };

                var stoken = tokenHandler.CreateJwtSecurityToken(tokenDescriptor);
                var token = tokenHandler.WriteToken(stoken);
                // return Content(HttpStatusCode.OK, token);
                return token;
            }
            catch(Exception ex)
            {
                return ex.InnerException.Message;
            }
        }

        public static ClaimsPrincipal GetPrincipal(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

                if (jwtToken == null)
                    return null;

                var symmetricKey = Convert.FromBase64String(Secret);

                var validationParameters = new TokenValidationParameters()
                {
                   RequireExpirationTime = true,
                   ValidateIssuer = false,
                   ValidateAudience = false,
                   IssuerSigningKey = new SymmetricSecurityKey(symmetricKey)
                };

                SecurityToken securityToken;
                var principal = tokenHandler.ValidateToken(token, validationParameters, out securityToken);

                return principal;
            }

            catch (Exception)
            {
                return null;
            }
        }
    }
}