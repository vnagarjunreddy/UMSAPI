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
   public class UserRole
    {
        [Key]
        public int UserRoleID { get; set; }

        [Required]
        public int? UserID { get; set; }

        [Required]
        public int? RoleID { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public string UpdatedBy { get; set; }

        public DateTime UpdatedDate { get; set; }

        public virtual User Users { get; set; }
        public virtual Role Roles { get; set; }

        [NotMapped]
        public string PcrId { get; set; }

        [NotMapped]
        public string DatabaseId { get; set; }
    }
}
