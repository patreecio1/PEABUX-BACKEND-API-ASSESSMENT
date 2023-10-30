

using HealthInsuranceERP.Core.Dtos;
using HealthInsuranceERP.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInsuranceERP.Core.Interfaces.Managers
{
    public interface IRoleManager
    {
        Task<RoleDto> Create(RoleDto role);
        Task<RoleDto> UpdateRole(RoleDto role);
        Task<RoleDto> Activate(long id);
        Task<RoleDto> Deactivate(long id);
        Task<Page<RoleDto>> GetRolePaginated(int pageNumber, int pageSize);
        Task<UserRoleDto[]> GetCurrentUserRole();
        Task<UserRoleDto[]> GetCurrentUserRole(string userId);
        Task<RoleDto[]> GetRoles();
    }
}
