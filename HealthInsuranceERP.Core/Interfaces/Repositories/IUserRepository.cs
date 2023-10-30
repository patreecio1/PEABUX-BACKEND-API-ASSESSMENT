
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
    public interface IUserRepository
    {
        Task<AppUserDto> CreateUser(AppUserDto userDto);
        Task<AppUserDto> GetUserByEmail(string email);
        Task<AppUserDto> UpdateConfirmation(string email);
        Task<AppUserDto> GetSuperAdmin();
        Task<bool> UpdateIsSuperAdmin(string id);
        Task<Page<AppUserDto>> GetUsers(int pageNumber, int pageSize);
        Task<AppUserDto> Deactivate(string userId);
        Task<AppUserDto> Activate(string userId);
        Task<AppUserDto> GetById(string userId);
        Task<AppUserDto> Update(AppUserDto model);
        Task<PermissionDto[]> GetCurrentUserPermission();
        Task<AppUserDto> Login(string email, string password);
        Task<RolePermissionDto[]> GetCurrentUserPermission(string userId);
      
        Task<int> UpdatePassword(string Email,string NewPassword, bool Mustchangepassword);
        Task<AppUserDto> CreateDefaultUser();
        Task<AppUserDto[]> GetAllUsers();
      
      //  Task<AppUserDto[]> GetManagers(long requesterRoleId);
       // Task<AppUserDto[]> GetManagers();
         Task<AppUserDto> UpdateLockCounter(string email);
        Task<AppUserDto> UpdateLastLogin(string email);
        Task<int> ReleaseLockedAccounts();
        Task<AppUserDto[]> GetByIds(string[] Ids);


    }
}
 