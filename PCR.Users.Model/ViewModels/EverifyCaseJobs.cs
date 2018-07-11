using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCR.Users.Model.ViewModels
{
   public  class EverifyCaseJobs
    {
        public int EverifyJobID { get; set; }
        public int EverifyCaseID { get; set; }
       
        public int EmployeeID { get; set; }
        public string JobType { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Ssn { get; set; }
        public string CaseNumber { get; set; }
        public string CurrentStateCode { get; set; }
        public int MessageCode { get; set; }
        public string EligibilityStatement { get; set; }
        public string EligibilityStatementDetails { get; set; }
        public string LetterTypeCode { get; set; }
        public string ResolutionCode { get; set; }
        public string EmployerCaseId { get; set; }
        public string VerificationStep { get; set; }
        public string ClosureCode { get; set; }
        public string ClosureCodeDesc { get; set; }
        public string StatusCode { get; set; }
        public string StatusMessage { get; set; }
        public bool Success { get; set; }
        public bool Deleted { get; set; }
        public string Request { get; set; }
        public string Response { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> LastModifiedOn { get; set; }
        public string LastModifiedBy { get; set; }
        public bool? EmployeeNotified { get; set; }

        public string LateReason { get; set; }

        public bool? Refferal { get; set; }

        public bool? CurrentlyEmployeed { get; set; }
    }
}
