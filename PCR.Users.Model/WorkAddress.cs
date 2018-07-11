using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PCR.Users.Models
{
    public class WorkAddress
    {
        [Key]
        public int WorkAddressID { get; set; }
        
        [StringLength(255)]
        public string Address1 { get; set; }

        [StringLength(255)]
        public string Address2 { get; set; }
        
        [StringLength(100)]
        public string StateId { get; set; }
        
        [StringLength(100)]
        public string CityId { get; set; }

        [StringLength(6)]
        public string Zip { get; set; }

        [StringLength(11)]
        public string WorkPhone { get; set; }

        [Required]
        public int RoleID { get; set; }
        public virtual Role Role { get; set; }                   // Foreign key of the Role table.
        
        [StringLength(100)]
        public string LocationID { get; set; }

        [Required]
        public int UserID { get; set; }                           // Foreign key of the User table.
        public virtual User User { get; set; }

        [NotMapped]
        public string DatabaseId { get; set; }
        [NotMapped]
        public string pcrId { get; set; }
        
    }
}
