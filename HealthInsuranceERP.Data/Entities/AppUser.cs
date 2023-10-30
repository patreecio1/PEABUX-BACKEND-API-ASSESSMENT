using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInsuranceERP.Data.Entities
{
    public class AppUser : Entity<string>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsSuperAdmin { get; set; }
        public string Password { get; set; }
  
        public int LockoutCounter { get; set; }
        public bool IsLockedOut { get; set; }
        public DateTime DateAccountLocked { get; set; }
        public bool MustChangePassword { get; set; }
        public DateTime? LastLogin { get; set; }

        public DateTime? ReleasedDate { get; set; }
        public String Message { get; set; }

        public Boolean IsAway { get; set; }
        public DateTime? AwayStartDate { get; set; }
        public DateTime? AwayEndate { get; set; }
    }
}
