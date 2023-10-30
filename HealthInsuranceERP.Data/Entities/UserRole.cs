using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace HealthInsuranceERP.Data.Entities
{
    public class UserRole : Entity<long>
    {
        public UserRole()
        {
        }
        public string UserId { get; set; }
        public long RoleId { get; set; }

        [ForeignKey("RoleId")]
        public Role Role { get; set; }

        [ForeignKey("UserId")]
        public AppUser User { get; set; }
    }
}
