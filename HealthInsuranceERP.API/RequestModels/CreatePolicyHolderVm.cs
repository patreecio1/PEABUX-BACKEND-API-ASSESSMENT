using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace HealthInsuranceERP.API.RequestModels
{
    public class CreatePolicyHolderVm : Model
    {
        [Required(ErrorMessage = "National ID is required")]

        public string NationalID { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Surname is required")]
        public string Surname { get; set; }
        [Required(ErrorMessage = "Date Of Birth is required")]
        public DateTime DateOfBirth { get; set; }
        [Required(ErrorMessage = "Policy Number is required")]
        public string PolicyNumber { get; set; }
    }
}
