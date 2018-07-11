using PCR.Users.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PCR.Users.API.ViewModels
{
    public class UserWorkAddress
    {
        [Required]
        [StringLength(255, ErrorMessage = "User name should not exceed more than 255 characters")]
        [EmailAddress(ErrorMessage = "Please enter valid UserName(Email address)")]
        public string UserName { get; set; }

        [Required]
        [StringLength(128, ErrorMessage = "Password should not exceed more than 128 characters")]
        public string Password { get; set; }

        public Boolean? EmailConfirmed { get; set; }

        public int AccessFailedCount { get; set; }

        public Boolean? LockOutEnabled { get; set; }

        [StringLength(15, ErrorMessage = "Phone number should not exceed more than 15 characters")]
        public string PhoneNumber { get; set; }

        public Boolean? PhoneNumberConfirmed { get; set; }

        //[Required]
        [StringLength(255, ErrorMessage = "Email address should not exceed more than 255 characters")]
        //[EmailAddress(ErrorMessage = "Please enter valid Email address")]
        public string EmailAddress { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "LastName should not exceed more than 100 characters")]
        public string LastName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "FirstName should not exceed more than 100 characters")]
        public string FirstName { get; set; }

        [StringLength(100, ErrorMessage = "MiddleName should not exceed more than 100 characters")]
        public string MiddleName { get; set; }

        public Boolean? IsDelete { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public string UpdatedBy { get; set; }

        public DateTime UpdatedDate { get; set; }

        public Boolean? Gender { get; set; }

        public string Mobile { get; set; }

        
        public string OtherName { get; set; }

        public DateTime? DOB { get; set; }

        [StringLength(100, ErrorMessage = "Reporting manager name should not exceed more than 100 characters")]
        public string ReportingAuthorityName { get; set; }

        [StringLength(255, ErrorMessage = "Reporting manager email address should not exceed more than 255 characters")]
        public string ReportingAuthorityEmail { get; set; }
                
        [StringLength(10, ErrorMessage = "Phone number should not exceed more than 10 characters")]
        public string ReportingAuthorityPhone { get; set; }


        public virtual ICollection<WorkAddress> workaddress { get; set; }



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
        public virtual Role Role { get; set; }       // foreign key of the Role table.
        
        [StringLength(100)]
        public string LocationID { get; set; }
        
        public int UserID { get; set; }

        public virtual UserDetails User { get; set; }

        public int? ClientID { get; set; }
        public string ClientName { get; set; }
        public string ConnectionString { get; set; }

        [NotMapped]
        public string PcrId { get; set; }

        [NotMapped]
        public string DatabaseId { get; set; }
    }
}