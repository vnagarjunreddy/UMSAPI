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
    public class DocumentPackage
    {
       
        [Key]
        public int DocumentPackageID { get; set; }
        [StringLength(100)]
        [Required(ErrorMessage = "Document title field is required.")]
        public string DocumentTitle { get; set; }
        [StringLength(255)]
        public string DocumentName { get; set; }

        [StringLength(15)]
        public string Category { get; set; }   // General / Policy

        [StringLength(260)]
        public string Path { get; set; }

        [StringLength(500)]
        public string Description { get; set; }
        [StringLength(100)]
        public string ContentType { get; set; }
        [Required]
        public Boolean IsFolder { get; set; }
        public Boolean IsDeleted { get; set; }
        [StringLength(255)]
        [Required]
        public string OwnerId { get; set; }
        public int ParentId { get; set; }
        public Boolean Isvalid { get; set; }
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidUpto { get; set; }
        [StringLength(255)]
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        [StringLength(255)]
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public byte[] UploadDocument { get; set; }

        [NotMapped]
        [StringLength(500)]
        public string DatabaseId { get; set; }
        [NotMapped]
        [StringLength(500)]
        public string PcrId { get; set; }

     

    }
}
