using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace HealthInsuranceERP.Data.Entities
{
    public class LoginAttemptLog : Entity<long>
    {
        public LoginAttemptLog()
        {
        }

        public string Email { get; set; }
    }
}
