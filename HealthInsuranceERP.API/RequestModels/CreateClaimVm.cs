using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using HealthInsuranceERP.Core.Dtos;

namespace HealthInsuranceERP.API.RequestModels
{
    public class CreateClaimVm : Model
    {
        [Required(ErrorMessage = "National ID is required")]

        public string NationalId { get; set; }
        [Required(ErrorMessage = "Expense is required")]


        public List<CreateExpenseVm> Expenses { get; set; }
    }
    
}
