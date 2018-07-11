using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PCR.Users.Model.ViewModels
{
    public class PCRTokens
    {  
        public string OauthToken { get; set; }

       
        public string RefreshToken { get; set; }

        public string DatabaseId { get; set; }

        public long  UserRecordId { get; set; }



    }
}