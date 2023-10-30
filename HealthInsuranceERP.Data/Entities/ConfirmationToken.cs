using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInsuranceERP.Data.Entities
{
    public class ConfirmationToken : Entity<int>
    {
        public string TokenId { get; set; }
        public string Token { get; set; }
        public string UserId { get; set; }
        public DateTime? ExpiredDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
