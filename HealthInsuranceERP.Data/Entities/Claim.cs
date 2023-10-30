using HealthInsuranceERP.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace HealthInsuranceERP.Data.Entities
{

        public class Claim : Entity<int>
    {
        public int ClaimId { get; set; }
        public string NationalId { get; set; }
        public string ClaimStatus { get; set; }
        public string ApprovalMessage { get; set; }

        public List<Expense> Expenses { get; set; }


    }

 
}
