

using HealthInsuranceERP.Core.Dtos;
using HealthInsuranceERP.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInsuranceERP.Core.Interfaces.Managers
{
    public interface IUserRoleManager
    {
        Task<UserRoleDto> GetByUserRole();
        Task RemoveRole(long userRoleId);
        Task<UserRoleDto> AddUserRole(UserRoleDto entity);
        Task<Page<UserRoleDto>> Get(int pageSize, int pageNumber);
    }
}
