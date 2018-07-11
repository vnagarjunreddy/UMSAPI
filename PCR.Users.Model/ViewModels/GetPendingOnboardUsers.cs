using PCR.Users.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCR.Users.Model.ViewModels
{
    public class GetPendingOnboardUsers
    {
        public int? RoleID { get; set; }

        public int Count { get; set; }

        public string UserName { get; set; }

        public string FullName { get; set; }

    }
}
