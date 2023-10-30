
using HealthInsuranceERP.Core.DTOs;
using HealthInsuranceERP.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInsuranceERP.Core.Dtos
{
    public class RolePermissionDto : BaseModel<long>
    {
        public RolePermissionDto()
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
        public string PermissionName { get; set; }
        public string RoleName { get; set; }
        public RoleDto Role { get; set; }
        public PermissionDto Permission { get; set; }
    }
}
