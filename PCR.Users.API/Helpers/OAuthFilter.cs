using Microsoft.IdentityModel.Tokens;
using PCR.Users.Data;
using PCR.Users.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Filters;
using System.Web.Http.Results;

namespace PCR.Users.API.Helpers
{
    public class OAuthFilter :  Attribute, IAuthenticationFilter
    {
        public string Realm { get; set; }
        public bool AllowMultiple => false;

        public async Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        {
            var request = context.Request;
            var authorization = request.Headers.Authorization;
            try
            {
                if (authorization == null || authorization.Scheme.ToLower() != "bearer")
                {
                    context.ErrorResult = new AuthenticationFailureResult("Missing access token", request);
                    return;
                }
                else if (string.IsNullOrEmpty(authorization.Parameter))
                {
                    context.ErrorResult = new AuthenticationFailureResult("Missing access token", request);
                    return;
                }
                else
                {
                    BaseFilter baseFilter = new BaseFilter(ConfigurationManager.AppSettings["PCRAppPath"]);
                    CGI4VB.OnboardingSessionManager sessionManager;
                    sessionManager = new CGI4VB.OnboardingSessionManager(authorization.Parameter);

                    if (sessionManager == null)
                    {
                        context.ErrorResult = new AuthenticationFailureResult("Invalid access token", request);
                        return;
                    }
                    //else
                    //{
                    //    var 
                    //}
                }
            }
            catch (Exception ex)
            {
                context.ErrorResult = new AuthenticationFailureResult("Invalid access token", request);
                return;
            }
        }

        public Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        {
            Challenge(context);
            return Task.FromResult(0);
        }

        private void Challenge(HttpAuthenticationChallengeContext context)
        {
            string parameter = null;

            if (!string.IsNullOrEmpty(Realm))
                parameter = "realm=\"" + Realm + "\"";

            context.ChallengeWith("Bearer", parameter);
        }

    }
}
    


