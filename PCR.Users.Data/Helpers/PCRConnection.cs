using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace PCR.Users.Data.Helpers
{
    public class PCRConnection : CGI4VB
    {
        public PCRConnection()
        {
            this.apppath = @".\PCRBIN";
        }
        public PCRConnection(string overrideDefaultAppPath)
        {
            this.apppath = overrideDefaultAppPath;
        }

        public override void CGI_Main()
        {
            throw new NotImplementedException();
        }

       
    }
}
