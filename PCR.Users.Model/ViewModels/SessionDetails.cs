using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCR.Users.Model.ViewModels
{
   public class DBCredentials
    {
        public int ClientID { get; set; }
    
        public string DBName { get; set; }
    
        public string DBUserId { get; set; }
       
        public string DBPassword { get; set; }

        public string DBServer { get; set; }
        
    }
}
