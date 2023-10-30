using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInsuranceERP.Core.Dtos
{
    public class ClaimDto
    {
        public ClaimDto()
        {
        }
        public int ClaimId { get; set; }
        public string NationalId { get; set; }
        public double TotalAmount { get; set; }
        public string ClaimStatus { get; set; }
        public string ApprovalMessage { get; set; }

        public List<ExpenseDto> Expenses { get; set; }
    }
}
