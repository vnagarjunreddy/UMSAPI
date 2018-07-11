using PCR.Users.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PCR.Users.Models
{
    public class UserDetails
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public string EmailAddress { get; set; }


        public string LastName { get; set; }


        public string ReportingAuthorityEmail { get; set; }


        public string ReportingAuthorityPhone { get; set; }

        public string FirstName { get; set; }

        public int UserID { get; set; }
    }
}