using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PCR.Users.API.ViewModels
{
    public class UserDTO
    {
        [Key]
        public int UserID { get; set; }

        [Required]
        [StringLength(255, ErrorMessage = "Email address should not exceed more than 255 characters")]
        [EmailAddress(ErrorMessage = "Please enter valid UserName(Email address)")]
        public string UserName { get; set; }

        [Required]
        [StringLength(128, ErrorMessage = "Password should not exceed more than 128 characters")]
        public string Password { get; set; }

        public Boolean EmailConfirmed { get; set; }

        public int AccessFailedCount { get; set; }

        public Boolean LockOutEnabled { get; set; }        

        [StringLength(15, ErrorMessage = "Phone number should not exceed more than 15 characters")]
        public string PhoneNumber { get; set; }

        public Boolean PhoneNumberConfirmed { get; set; }

       
        [StringLength(255, ErrorMessage = "Email address should not exceed more than 255 characters")]
        [EmailAddress(ErrorMessage = "Please enter valid Email address")]
        public string EmailAddress { get; set; }

        [StringLength(100, ErrorMessage ="LastName should not exceed more than 100 characters")]
        public string LastName { get; set; }

        [StringLength(100, ErrorMessage = "FirstName should not exceed more than 100 characters")]
        public string FirstName { get; set; }

        [StringLength(100, ErrorMessage = "MiddleName should not exceed more than 100 characters")]
        public string MiddleName { get; set; }

        public Boolean IsDelete { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public string UpdatedBy { get; set; }

        public DateTime UpdatedDate { get; set; }

        [NotMapped]
        public string PcrId { get; set; }

        [NotMapped]
        public string databaseId { get; set; }
    }
}