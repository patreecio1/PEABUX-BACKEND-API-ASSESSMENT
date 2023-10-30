
using HealthInsuranceERP.Core.DTOs;
using HealthInsuranceERP.Core.Interfaces.Managers;
using HealthInsuranceERP.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInsuranceERP.Core.Managers
{
    public class PermissionManager : IPermissionManager
    {
        private readonly IPermissionRepository permissionRepository;
        public PermissionManager(IPermissionRepository permissionRepository)
        {
            this.permissionRepository = permissionRepository;
        }

        public async Task<PermissionDto[]> GetPermissions()
        {
            return await permissionRepository.GetPermissions();
        }
    }
}
