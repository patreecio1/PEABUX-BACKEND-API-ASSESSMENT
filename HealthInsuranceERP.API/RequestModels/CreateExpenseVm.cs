using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using HealthInsuranceERP.Core.Dtos;

namespace HealthInsuranceERP.API.RequestModels
{
    public class CreateExpenseVm : Model
    {
        public string Name { get; set; }
        public double Amount { get; set; }
        public DateTime DateOfExpense { get; set; }
    }
    
}
