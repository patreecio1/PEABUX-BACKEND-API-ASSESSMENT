
using HealthInsuranceERP.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInsuranceERP.Core.Interfaces.Managers
{
    public interface IPermissionManager
    {
        Task<PermissionDto[]> GetPermissions();
    }
}
