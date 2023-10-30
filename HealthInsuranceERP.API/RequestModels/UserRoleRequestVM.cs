using System.ComponentModel.DataAnnotations;

namespace HealthInsuranceERP.API.RequestModels
{
    public class UserRoleRequestVM : Model
    {
        [Required(ErrorMessage = "User is required")]
        public string UserId { get; set; }

        [Required(ErrorMessage = "Role is required")]
        public long RoleId { get; set; }
    }
}
