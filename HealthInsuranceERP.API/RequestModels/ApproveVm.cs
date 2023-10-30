using System.ComponentModel.DataAnnotations;

namespace HealthInsuranceERP.API.RequestModels
{
    public class ApproveVm
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Message is required")]
        public string Message { get; set; }
    }
}
