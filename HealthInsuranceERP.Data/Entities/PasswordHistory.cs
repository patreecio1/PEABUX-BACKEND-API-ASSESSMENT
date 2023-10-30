using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInsuranceERP.Data.Entities
{
    public class PasswordHistory : Entity<long>
    {
        public string UserId { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public bool IsCurrentPassword { get; set; }

    }
}
