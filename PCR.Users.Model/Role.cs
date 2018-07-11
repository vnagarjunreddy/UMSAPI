using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCR.Users.Models
{
    public class Role
    {
        [Key]
        public int RoleID { get; set; }

        [Required(ErrorMessage ="Role name is required.")]
        [StringLength(50, ErrorMessage = "Role name should not exceed more than 50 characters.")]
        public string RoleName { get; set; }

        [StringLength(500, ErrorMessage = "Description should not exceed more than 500 characters.")]
        public string Description { get; set; }

        public Boolean? IsActive { get; set; }

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
