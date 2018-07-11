using PCR.Users.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCR.Users.Model.ViewModels
{
    public class EverifyEmloyees
    {

        public int  UserID { get; set; }
        
        public string UserName { get; set; }
        
        public string LastName { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }
        
        public string SSN { get; set; }

        public string CaseNumber { get; set; }

        public string CaseStatus { get; set; }
        
    }
}

