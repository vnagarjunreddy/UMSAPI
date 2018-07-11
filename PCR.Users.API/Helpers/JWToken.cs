//using Microsoft.IdentityModel.Tokens;
//using PCR.Users.Services;
//using System;
//using System.Collections.Generic;
//using System.IdentityModel.Tokens.Jwt;
//using System.Net.Http;
//using System.Net.Http.Headers;
//using System.Security.Claims;
//using System.Security.Principal;
//using System.Threading;
//using System.Threading.Tasks;
//using System.Web.Http;
//using System.Web.Http.Filters;

//namespace PCR.Users.API.Helpers
//{
//    public class JwtAuthenticationAttribute : Attribute, IAuthenticationFilter
//    {

//        bool IFilter.AllowMultiple
//        {
//            get
//            {
//                throw new NotImplementedException();
//            }
//        }

//        public const string Secret = "856FECBA3B06519C8DDDBC80BB080553"; // your symetric

//        [NonAction]
//        public static bool ValidateToken(string token, out string username)
//        {
//            username = null;
//            var simplePrinciple = GetPrincipal(token);
//            if (simplePrinciple != null)
//            {
//                var identity = simplePrinciple.Identity as ClaimsIdentity;

//                if (identity == null)
//                    return false;  //Content(HttpStatusCode.NotFound, false);

//                if (!identity.IsAuthenticated)
//                    return false; // Content(HttpStatusCode.NotFound, false);

//                var usernameClaim = identity.FindFirst(ClaimTypes.Name);
//                username = usernameClaim?.Value;

//                if (string.IsNullOrEmpty(username))
//                    return false;//Content(HttpStatusCode.NotFound, false);



//                return true; //Content(HttpStatusCode.OK, true);
//            }
//            else
//                return false;
//        }

//        protected Task<IPrincipal> AuthenticateJwtToken(string token)
//        {
//            string username;
//            if (ValidateToken(token, out username))
//            {
//                based on username to get more information from database in order to build local identity
//               var claims = new List<Claim>
//           {
//                new Claim(ClaimTypes.Name, username)
//                 Add more claims if needed: Roles, ...
//            };

//            var identity = new ClaimsIdentity(claims, "Jwt");
//            IPrincipal user = new ClaimsPrincipal(identity);

//            return Task.FromResult(user);
//        }

//            return Task.FromResult<IPrincipal>(null);
//        }

//    public static ClaimsPrincipal GetPrincipal(string token)
//    {
//        try
//        {
//            var tokenHandler = new JwtSecurityTokenHandler();
//            var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

//            if (jwtToken == null)
//                return null;

//            var symmetricKey = Convert.FromBase64String(Secret);

//            var validationParameters = new TokenValidationParameters()
//            {
//                RequireExpirationTime = true,
//                ValidateIssuer = false,
//                ValidateAudience = false,
//                IssuerSigningKey = new SymmetricSecurityKey(symmetricKey),
//                ValidateLifetime = true
//            };

//            SecurityToken securityToken;
//            var principal = tokenHandler.ValidateToken(token, validationParameters, out securityToken);

//            return principal;
//        }

//        catch
//        {
//            return null;
//        }
//    }

//    async Task IAuthenticationFilter.AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
//    {
//        HttpRequestMessage request = context.Request;

//        string accessToken = request.GetQueryString("accessToken");

//        if (string.IsNullOrEmpty(accessToken))
//        {
//            context.ErrorResult = new AuthenticationFailureResult("Invalid access token", request);
//        }

//        IPrincipal principal = GetPrincipal(accessToken);

//        if (principal == null)
//        {
//            context.ErrorResult = new AuthenticationFailureResult("Invalid credentials", request);

//        }
//        else
//        {
//            context.Principal = principal;
//        }

//    }

//    async Task IAuthenticationFilter.ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
//    {
//        context.ChallengeWith()
//        }
//}
//}
