using Microsoft.IdentityModel.Tokens;
using PCR.Users.Services;
using System;
using System.Collections.Generic;
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
    public class BaseFilter : CGI4VB
    {
        protected CGI4VB.OnboardingSessionManager sessionManager;

        public BaseFilter()
        {
            this.apppath = @".\PCRBIN";
        }
        public BaseFilter(string overrideDefaultAppPath)
        {
            this.apppath = overrideDefaultAppPath;
        }

        public override void CGI_Main()
        {
            throw new NotImplementedException();
        }

        

    }
}
