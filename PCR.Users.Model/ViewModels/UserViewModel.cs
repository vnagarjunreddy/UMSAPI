using PCR.Users.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCR.Users.Model.ViewModels
{
    public class UserViewModel
    {
        [Required]
        [StringLength(255, ErrorMessage = "Email address should not exceed more than 255 characters")]
        [EmailAddress(ErrorMessage = "Please enter valid UserName(Email address)")]
        public string UserName { get; set; }

        
        public Boolean? EmailConfirmed { get; set; }

        public int AccessFailedCount { get; set; }

        public Boolean? LockOutEnabled { get; set; }

        [StringLength(10, MinimumLength = 10, ErrorMessage = "Phone number should be equals to 10 digits.")]        
        public string PhoneNumber { get; set; }

        public Boolean? PhoneNumberConfirmed { get; set; }

        [StringLength(255, ErrorMessage = "Email address should not exceed more than 255 characters.")]
        //[EmailAddress(ErrorMessage = "Please enter valid Email address")]
        public string EmailAddress { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "LastName should not exceed more than 100 characters.")]
        public string LastName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "FirstName should not exceed more than 100 characters.")]
        public string FirstName { get; set; }

        [StringLength(100, ErrorMessage = "MiddleName should not exceed more than 100 characters.")]
        public string MiddleName { get; set; }

        public Boolean? IsDelete { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public string UpdatedBy { get; set; }

        public DateTime UpdatedDate { get; set; }

        public Boolean? Gender { get; set; }

        [StringLength(10, MinimumLength = 10, ErrorMessage = "Mobile number should be equals to 10 digits.")]
        public string Mobile { get; set; }

        public string OtherName { get; set; }

        public DateTime? DOB { get; set; }

        [StringLength(100, ErrorMessage = "Reporting manager name should not exceed more than 100 characters.")]
        public string ReportingAuthorityName { get; set; }

        [StringLength(255, ErrorMessage = "Reporting manager email address should not exceed more than 255 characters.")]
        [EmailAddress(ErrorMessage = "Please enter valid reporting manager email address.")]
        public string ReportingAuthorityEmail { get; set; }

        [StringLength(10, MinimumLength = 10, ErrorMessage = "Reporting manager phone number should be equals to 10 digits.")]
        public string ReportingAuthorityPhone { get; set; }
        
        [Key]
        public int WorkAddressID { get; set; }

        [StringLength(255)]
        public string Address1 { get; set; }

        [StringLength(255)]
        public string Address2 { get; set; }

        // [Required]
        [StringLength(100)]
        public string StateId { get; set; }

        //  [Required]
        [StringLength(100)]
        public string CityId { get; set; }

        [StringLength(5, MinimumLength = 5, ErrorMessage = "Zip code should be equals to 5 digits.")]
        public string Zip { get; set; }

        [StringLength(11, MinimumLength = 11, ErrorMessage = "Work phone should be equals to 11 digits.")]
        public string WorkPhone { get; set; }

        [Required(ErrorMessage = "RoleID field is required.")]
        public int? RoleID { get; set; }
        //  public virtual Role Role { get; set; }

        // [Required]
        [StringLength(100)]
        public string LocationID { get; set; }

        public int UserID { get; set; }
        //  public virtual User User { get; set; }

        public string ClientID { get; set; }
        public string ConnectionString { get; set; }

        public string DatabaseId { get; set; }
        public string PcrId { get; set; }
        
        public long? PcrRecordId { get; set; }

        [StringLength(100)]
        public string PcrDatabaseId { get; set; }

        public bool IsPcrUser { get; set; }

        public string AccessToken { get; set; }

        public string OAuthToken { get; set; }

        public string RefreshToken { get; set; }
        
        public ICollection<LinkedPCRUsers> LinkedPcrUsers { get; set; }

        public int? JobTitleID { get; set; }
    }
}
