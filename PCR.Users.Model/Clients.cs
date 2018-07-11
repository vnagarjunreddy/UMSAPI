using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PCR.Users.Models
{
    public class Clients
    {
        [Key]
        public int ClientID { get; set; }

        [Required]
        [StringLength(100), Column(TypeName = "varchar")]
        public string ClientName { get; set; }

        [StringLength(500), Column(TypeName = "varchar")]
        public string UrlName { get; set; }

        [StringLength(100), Column(TypeName = "varchar")]
        public string ContactPerson { get; set; }

        [StringLength(255), Column(TypeName = "varchar")]
        [EmailAddress]
        public string ContactEmail { get; set; }

        [StringLength(10,MinimumLength =10), Column(TypeName = "char")]
        public string ContactPhone { get; set; }

        [StringLength(500), Column(TypeName = "varchar")]
        public string CorporateAddress { get; set; }

        [StringLength(500), Column(TypeName = "varchar")]
        public string MailingAddress { get; set; }

        [StringLength(500), Column(TypeName = "varchar")]
        public string BillingAddress { get; set; }

        [StringLength(255), Column(TypeName = "varchar")]
        public string BillingMail { get; set; }

        [StringLength(500), Column(TypeName = "varchar")]
        public string Comments { get; set; }

        [Required]
        [StringLength(100), Column(TypeName = "varchar")]
        public string ClientCaption { get; set; }

        public byte[] ClientLogo { get; set; }

        [StringLength(255), Column(TypeName = "varchar")]
        public string ClientWelcomeMessage { get; set; }

        public byte[] ClientWelcomeLogo { get; set; }

        [StringLength(500), Column(TypeName = "varchar")]
        public string ConnectionString { get; set; }

        [StringLength(255), Column(TypeName = "varchar")]
        public string CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        [StringLength(255), Column(TypeName = "varchar")]
        public string ModifiedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }
        
       // [Required]
        [StringLength(500), Column(TypeName = "varchar")]
        public string DatabaseId { get; set; }

        [NotMapped]
        [StringLength(500)]
        public string PcrId { get; set; }
        public bool IsDelete { get; set; }
        public int? DeletedBy { get; set; }

        public string DBName { get; set; }

        public string DBUserId { get; set; }

        public string DBPassword { get; set; }

        public string DBServer { get; set; }

    }
}
