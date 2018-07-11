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
    public class I9AcceptableDocumentPackages
    {
        [Key]
        public int AcceptableDocumentPackageID { get; set; }

        public int DocumentPackageID { get; set; }

        public int UserId { get; set; }

        public int ParentID { get; set; }

        // public virtual I9DocTypes I9DocTypes { get; set; }

        [StringLength(255)]
        public string CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        [StringLength(255)]
        public string ModifiedBy { get; set; }

        [StringLength(255)]
        public string I9ListCategory { get; set; }


        public string DocumentTitle { get; set; }

        public DateTime? ModifiedDate { get; set; }

        [NotMapped]
        [StringLength(500)]
        public string DatabaseId { get; set; }

        [NotMapped]
        [StringLength(500)]
        public string PcrId { get; set; }
    }
}
