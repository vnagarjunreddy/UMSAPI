using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PCR.Users.API.ViewModels
{
    public class ResetPassword
    {
        public int UserID { get; set; }

        [Required]
        [StringLength(128, ErrorMessage = "Password should not exceed more than 128 characters")]
        public string Password { get; set; }

        [NotMapped]
        public string ClientID { get; set; }
        [NotMapped]
        public string ConnectionString { get; set; }

        [NotMapped]
        public string DatabaseId { get; set; }
        [NotMapped]
        public string pcrId { get; set; }

    }
}