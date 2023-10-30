using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace HealthInsuranceERP.API.RequestModels
{
    public class ChangePasswordVm : Model
    {
        [Required(ErrorMessage = "Current password is required")]
        public string CurrentPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "New Password")]
        [Required(ErrorMessage = "The Password is required")]
        [MinLength(6, ErrorMessage = "Password should be atleast 6 characters")]
        [MaxLength(12, ErrorMessage = "Password should not be more than 12 characters")]
        public string NewPassword { get; set; }


    }
}
