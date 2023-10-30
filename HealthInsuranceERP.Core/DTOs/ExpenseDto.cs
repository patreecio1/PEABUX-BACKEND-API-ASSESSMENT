using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInsuranceERP.Core.Dtos
{
    public class ExpenseDto
    {
        public ExpenseDto()
        {
        }
        public int ExpenseId { get; set; }
        public string Name { get; set; }
        public double Amount { get; set; }
        public DateTime DateOfExpense { get; set; }
        public int ClaimId { get; set; } // Foreign key to associate with a claim
    }
}
