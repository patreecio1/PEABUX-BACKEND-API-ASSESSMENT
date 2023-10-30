
using HealthInsuranceERP.Core.Dtos;
using HealthInsuranceERP.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace HealthInsuranceERP.Core.Interfaces.Repositories
{
    public interface IPermissionRepository
    {
        Task<PermissionDto[]> GetPermissions();
        Task<PermissionDto> GetById(long permissionId);
        Task<RolePermissionDto[]> GetSuperuserPermissions();

    }
}
