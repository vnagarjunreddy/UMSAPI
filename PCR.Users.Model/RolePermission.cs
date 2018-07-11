using PCR.Users.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCR.Users.Models
{
    public class RolePermission
    {
        [Key]
        public int RolePermissionID { get; set; }

        [Required]
        public int? RoleID { get; set; }

        [Required]
        public int? PermissionID { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public string UpdatedBy { get; set; }

        public DateTime UpdatedDate { get; set; }

        //public virtual Role Roles { get; set; }
        //public virtual Permission Permissions { get; set; }

        [NotMapped]
        public string PcrId { get; set; }

        [NotMapped]
        public string DatabaseId { get; set; }
    }
}
