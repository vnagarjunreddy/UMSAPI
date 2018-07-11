using PCR.Users.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCR.Users.Model.ViewModels
{
    public class DashboardDetails
    {
        public int PendingOnboardingCount { get; set; }

        public int PendingE_VerifyCount { get; set; }

        public int TotalEmployees { get; set; }

        public int RecentEmployeesCount { get; set; }

    }
}
