using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCR.Users.Model.ViewModels
{
    public class SessionDetails
    {
        public int UserId { get; set; }

        public string UserName { get; set; }

        public int RoleID { get; set; }

        public string Email { get; set; }

        public string databaseId { get; set; }
        public string DatabaseId() { return databaseId; }

        public int UserRecordId() { return UserId; }

        public bool? IsFirstLogin { get; set; }

    }

    
}
