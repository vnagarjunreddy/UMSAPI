
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PCR.Users.API.ViewModels
{
    public class ForgetPassword
    {
        public int? UserID { get; set; }

        [Required]
        [StringLength(255, ErrorMessage = "Email address should not exceed more than 255 characters")]
        [EmailAddress(ErrorMessage = "Please enter valid UserName(Email address)")]
        public string UserName { get; set; }
                
        [NotMapped]
        public string ClientID { get; set; }
        [NotMapped]
        public string ClientName { get; set; }
        [NotMapped]
        public string ConnectionString { get; set; }

        [NotMapped]
        public string DatabaseId { get; set; }
        [NotMapped]
        public string PcrId { get; set; }

    }
}
