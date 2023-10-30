
using HealthInsuranceERP.Core.Dtos;
using HealthInsuranceERP.Core.DTOs;
using HealthInsuranceERP.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace HealthInsuranceERP.Core.Interfaces.Repositories
{
    public interface IRolePermissionRepository
    {
        Task<RolePermissionDto> GetById(long id);
        Task<RolePermissionDto> Activate(long id);
        Task<PermissionDto[]> GetPermissions(long roleId);
        Task<Page<RolePermissionDto>> GetByRoleId(long roleId, int pageNumber, int pageSize);
        Task<RolePermissionDto> Update(RolePermissionDto result);
        Task<RolePermissionDto> Create(RolePermissionDto result);
        Task<RolePermissionDto[]> GetPermissionsforRoleId(long roleId);
        Task<int> AddRolePermissions(long roleId, RolePermissionModel rolePermissionRequest);

        Task<RolePermissionDto[]> GetPermissionsforSuperAdmin();
    }
}
