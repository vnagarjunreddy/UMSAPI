using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCR.Users.Models
{
   public class Permission
    {
        [Key]
        public int PermissionID { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "PermissionName should not exceed more than 50 characters")]
        public string PermissionName { get; set; }

        [StringLength(500, ErrorMessage = "Description should not exceed more than 500 characters")]
        public string Description { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public string UpdatedBy { get; set; }

        public DateTime UpdatedDate { get; set; }

        [NotMapped]
        public string PcrId { get; set; }

        [NotMapped]
        public string DatabaseId { get; set; }
    }
}
