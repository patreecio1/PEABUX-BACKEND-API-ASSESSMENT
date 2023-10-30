
using HealthInsuranceERP.Core.Dtos;
using HealthInsuranceERP.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInsuranceERP.Core.Interfaces.Managers
{
    public interface IUserManager
    {
        Task<AppUserDto> CreateUser(AppUserDto model);
        Task<Page<AppUserDto>> GetUsers(int pageNumber, int pageSize);
        Task<AppUserDto> Deactivate(string userId);
        Task<AppUserDto> Activate(string userId);
        Task<AppUserDto> Update(AppUserDto model);
        Task<AppUserDto[]> GetAllUsers();
       
        Task<UserRoleDto> AssignRoletoUser(string userId, long roleId);
      
        Task<int> AssignMultipleRolestoUser(string userId, long[] roleId);

    }
}
