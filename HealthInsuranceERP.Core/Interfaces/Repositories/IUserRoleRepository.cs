

using HealthInsuranceERP.Core.Dtos;
using HealthInsuranceERP.Core.Enums;
using HealthInsuranceERP.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace HealthInsuranceERP.Core.Interfaces.Repositories
{
    public interface IUserRoleRepository
    {
        Task<UserRoleDto> Get(string userId, long roleId);
        Task<UserRoleDto> Create(UserRoleDto entity);
        Task<Page<UserRoleDto>> GetPaginated(int pageNumber, int pageSize);
        Task<UserRoleDto> GetByUserId(string userId);
        Task<UserRoleDto> GetById(long userRoleId);
        Task<int> Remove(long userRoleId);
        Task<RolePermissionDto[]> GetCurrentUserPermission();
        Task<RolePermissionDto[]> GetCurrentUserPermission(string userId);
        Task<bool> IsPermitted(PermissionEnum permission);
        Task<UserRoleDto[]> GetRolesforUser(string userId);

        Task<int> Create(string userId, long[] roleIds);
        Task<int> Remove(string userId, long userRoleId);
        Task<UserRoleDto[]> GetUserRolesByRoleId(long roleId);


    }
}
