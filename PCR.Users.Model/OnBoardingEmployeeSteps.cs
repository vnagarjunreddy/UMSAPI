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
    public class OnBoardingEmployeeSteps
    {
        [Key]
        public int OnBoardingEmployeeStepId { get; set; }

        [Required]
        public int OnboardStageId { get; set; }

        [Required]
        public int UserID { get; set; }
        
        [StringLength(255), Column(TypeName = "varchar")]
        public string CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        [StringLength(255), Column(TypeName = "varchar")]
        public string UpdatedBy { get; set; }

        public DateTime UpdatedDate { get; set; }

    }
}

