using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace HealthInsuranceERP.Data.Entities
{
    public class RolePermission : Entity<long>
    {
        public RolePermission()
        {
        }

        public long RoleId { get; set; }
        public long PermissionId { get; set; }
        public bool CanAdd { get; set; }
        public bool CanEdit { get; set; }
        public bool CanDelete { get; set; }
        public bool CanRead { get; set; }
        public bool CanApprove { get; set; }
        public bool CanTerminate { get; set; }
        [ForeignKey("RoleId")]
        public Role Role { get; set; }

        [ForeignKey("PermissionId")]
        public Permission Permission { get; set; }
    }
}
