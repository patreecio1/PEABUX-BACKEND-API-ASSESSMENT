using System.ComponentModel.DataAnnotations;

namespace HealthInsuranceERP.API.RequestModels
{
    public class MultipleUserRoleRequestVM : Model
    {
        [Required(ErrorMessage = "User is required")]
        public string UserId { get; set; }

        public long[] RoleIds { get; set; }
    }
}
 
