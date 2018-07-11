using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCR.Users.Models
{
    public class LinkedPCRUsers
    {
        [Key]
        public int LinkedPCRUsersID { get; set; }

        [Required]
        public int UserID { get; set; }

        //public virtual User Users { get; set; }

        [Required]
        public string UserName { get; set; }
        
        public long? PcrRecordId { get; set; }

        [StringLength(100)]
        public string PcrDatabaseId { get; set; }

        public bool IsPcrUser { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public string UpdatedBy { get; set; }

        public DateTime UpdatedDate { get; set; }
    }
}
