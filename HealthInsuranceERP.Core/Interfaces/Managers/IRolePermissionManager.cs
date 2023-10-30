


using HealthInsuranceERP.Core.Dtos;
using HealthInsuranceERP.Core.DTOs;
using HealthInsuranceERP.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInsuranceERP.Core.Interfaces.Managers
{
    public interface IRolePermissionManager
    {
        Task<PermissionDto[]> GetUnAddedPermission(long roleId);
        Task<Page<RolePermissionDto>> GetRolePermissionByRoleId(long roleId, int pageNumber, int pageSize);
        Task<RolePermissionDto> Create(RolePermissionDto result);
        Task<RolePermissionDto> Update(RolePermissionDto result);
        Task<RolePermissionDto> Activate(long id);
        Task<RolePermissionDto> Deactivate(long id);
        Task<RolePermissionDto[]> GetPermissionsforRoleId(long roleId);
        Task<int> AddRolePermissions(long roleId, RolePermissionModel rolePermissionRequest);
    }
}
