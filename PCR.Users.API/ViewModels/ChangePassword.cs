using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PCR.Users.API.ViewModels
{
    public class ChangePassword
    {        
        public int UserID { get; set; }
		
		[Required]
        [StringLength(128, ErrorMessage = "OldPassword should not exceed more than 128 characters")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(128, ErrorMessage = "NewPassword should not exceed more than 128 characters")]
        public string NewPassword { get; set; }

        [NotMapped]
        public string PcrId { get; set; }

        [NotMapped]
        public string DatabaseId { get; set; }

    }
}