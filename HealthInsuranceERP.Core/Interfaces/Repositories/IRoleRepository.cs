
using HealthInsuranceERP.Core.Dtos;
using HealthInsuranceERP.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace HealthInsuranceERP.Core.Interfaces.Repositories
{
    public interface IRoleRepository
    {
        Task<RoleDto> GetById(long id);
        Task<RoleDto> Activate(long id);
        Task<RoleDto> Deactivate(long id);
        Task<RoleDto> Update(RoleDto model);
        Task<RoleDto> GetByRoleName(string roleName);
        Task<RoleDto> Create(RoleDto model);
        Task<Page<RoleDto>> GetPaginated(int pageNumber, int pageSize);
        Task<UserRoleDto[]> GetByUserId(string userId);
        Task<RoleDto[]> GetRoles();
        
    }
}
