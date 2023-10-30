using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInsuranceERP.Data.Entities
{
    public class Permission : Entity<long>
    {
        public Permission()
        {
        }

        public string PermissionName { get; set; }
        public string PermissionDisplayName { get; set; }
        public string PermissionDescription { get; set; }
    }
}
