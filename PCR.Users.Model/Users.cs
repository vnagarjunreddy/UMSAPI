using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCR.Users.Models
{
    public class User
    {
        [Key]
        public int UserID { get; set; }

        [Required]
        [StringLength(60, ErrorMessage = "User name should not exceed more than 60 characters.")]
        [EmailAddress(ErrorMessage = "Please enter valid User Name (Email address).")]
        public string UserName { get; set; }

        [Required]
        [StringLength(128, ErrorMessage = "Password should not exceed more than 128 characters.")]
        public string Password { get; set; }

        public Boolean? EmailConfirmed { get; set; }

        public int AccessFailedCount { get; set; }

        public Boolean? LockOutEnabled { get; set; }

        [StringLength(10, ErrorMessage = "Phone number should not exceed more than 10 characters.")]
        public string PhoneNumber { get; set; }

        public Boolean? PhoneNumberConfirmed { get; set; }

        //[Required]
        //[EmailAddress(ErrorMessage = "Please enter valid User Name (Email address).")]
        public string EmailAddress { get; set; }
        public byte[] ProfileImage { get; set; }
        [Required]
        
        [StringLength(60, ErrorMessage = "Last Name should not exceed more than 60 characters.")]
        public string LastName { get; set; }

        [Required]
        [StringLength(25, ErrorMessage = "First Name should not exceed more than 25 characters.")]
        public string FirstName { get; set; }

        [StringLength(1, ErrorMessage = "Middle Name should not exceed more than 1 character.")]
        public string MiddleName { get; set; }

        public Boolean? IsDelete { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public string UpdatedBy { get; set; }

        public DateTime UpdatedDate { get; set; }

        public Boolean? Gender { get; set; }

        [StringLength(10, ErrorMessage = "Phone number should not exceed more than 10 characters.")]
        public string Mobile { get; set; }
        [StringLength(40, ErrorMessage = "Other Name should not exceed more than 40 characters.")]
        public string OtherName { get; set; }

        public DateTime? DOB { get; set; }

        [StringLength(100, ErrorMessage = "Reporting manager name should not exceed more than 100 characters.")]
        public string ReportingAuthorityName { get; set; }

        [StringLength(255, ErrorMessage = "Reporting manager email address should not exceed more than 255 characters.")]
        [EmailAddress(ErrorMessage = "Please enter valid Email address.")]
        public string ReportingAuthorityEmail { get; set; }

        [StringLength(10, ErrorMessage = "Phone number should not exceed more than 10 characters.")]
        public string ReportingAuthorityPhone { get; set; }

        public virtual ICollection<WorkAddress> workaddress { get; set; }

        [NotMapped]
        public int? ClientID { get; set; }
        [NotMapped]
        public string ConnectionString { get; set; }

        [NotMapped]
        public string PcrId { get; set; }

        [NotMapped]
        public string DatabaseId { get; set; }

        [StringLength(100)]
        public string PCRUserName { get; set; }

        public long? PcrRecordId { get; set; }

        [StringLength(100)]
        public string PcrDatabaseId { get; set; }

        public bool IsPcrUser { get; set; }

        public bool IsOnboardCompleted { get; set; }

        public bool IsManagerApproved { get; set; }

        public ICollection<LinkedPCRUsers> LinkedPcrUsers { get; set; }

        public bool? IsFirstLogin { get; set; }

        public int? JobTitleID { get; set; }
    }
}
