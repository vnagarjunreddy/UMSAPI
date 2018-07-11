using PCR.Users.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCR.Users.Model.ViewModels
{
    public class UserMonthWiseCount
    {
        public string MonthName { get; set; }

        public int UserCount { get; set; }

    }
}
