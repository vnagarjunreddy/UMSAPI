using PCR.Users.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCR.Users.Model.ViewModels
{
    public class NotificationTemplete
    {

        [Key]
        public int Notification_settingsID { get; set; }
        public int userid { get; set; }

        public string TemplatePath { get; set; }

        [Display(Name = "Email Subject")]
        public string EmailSubject { get; set; }
        public string ToEmailAddresses { get; set; }
        public int NotificationID { get; set; }

        public string Template { get; set; }
        public string UserName { get; set; }


        [NotMapped]
        public int ToUserId { get; set; }
        [NotMapped]
        public int FromUserid { get; set; }

        [NotMapped]
        public string clientAddress { get; set; }

        [NotMapped]
        public string adminAddress { get; set; }

        public int? NotifyID { get; set; }

        public bool IsSuperAdmin { get; set; }

        public bool IsClientAdmin { get; set; }

        public bool IsManager { get; set; }

        public bool IsEmployee { get; set; }


        [NotMapped]
        public string Employeeaddress { get; set; }

        [NotMapped]
        public string managertemplate { get; set; }

        [NotMapped]
        public string employeeName { get; set; }

        [NotMapped]
        public string managerAddress { get; set; }
    }
}

