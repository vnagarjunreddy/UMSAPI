using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace PCR.Users.Services.Helpers
{
    public class OnboardingCGI4VB : CGI4VB
    {
        public OnboardingCGI4VB()
            : base(false)
        {

        }

        public override void CGI_Main()
        {
            throw new NotImplementedException();
        }

    }

    public class FromPCRConnection
    {
        protected CGI4VB.OnboardingSessionManager sessionManager;
        protected OnboardingCGI4VB cgi;

        public FromPCRConnection()
        {
            cgi = new OnboardingCGI4VB();
            cgi.InitCgi();
            cgi.apppath = ConfigurationManager.AppSettings["PCRAppPath"];
            cgi.CGI_RemoteAddr = "127.0.0.1";
        }

      
    }
}
