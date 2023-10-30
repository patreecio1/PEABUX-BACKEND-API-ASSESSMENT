

using HealthInsuranceERP.Core.Dtos;
using HealthInsuranceERP.Core.Exceptions;
using HealthInsuranceERP.Core.Interfaces.Managers;
using HealthInsuranceERP.Core.Interfaces.Repositories;
using HealthInsuranceERP.Core.Interfaces.Services;
using HealthInsuranceERP.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInsuranceERP.Core.Managers
{
    public class UserRoleManager : IUserRoleManager
    {
        private readonly IHttpContextService httpContextService;
        private readonly IRoleRepository roleRepository;
        private readonly IUserRepository userRepository;
        private readonly IUserRoleRepository userRoleRepository;
        public UserRoleManager(IUserRoleRepository userRoleRepository, IUserRepository userRepository,
            IRoleRepository roleRepository, IHttpContextService httpContextService)
        {
            this.httpContextService = httpContextService;
            this.roleRepository = roleRepository;
            this.userRepository = userRepository;
            this.userRoleRepository = userRoleRepository;
        }

        public async Task<UserRoleDto> AddUserRole(UserRoleDto entity)
        {
            AppUserDto appUserDto = await userRepository.GetById(entity.UserId);

            if (appUserDto == null)
                throw new BadRequestException("User does not exist");
            RoleDto roleDto = await roleRepository.GetById(entity.RoleId);
            if (roleDto == null)
                throw new BadRequestException("Role does not exist");

            UserRoleDto userRole = await userRoleRepository.GetByUserId(entity.UserId);

            if (userRole != null)
                throw new BadRequestException("User aready have a role");

            userRole = await userRoleRepository.Create(entity);

            if (userRoleRepository == null)
                throw new BadRequestException("Request failed. Kindly retry");

            return userRole;

        }

        public async Task<Page<UserRoleDto>> Get(int pageSize, int pageNumber)
        {
            return await userRoleRepository.GetPaginated(pageNumber, pageSize);
        }

        public async Task<UserRoleDto> GetByUserRole()
        {
            string userId = httpContextService.CurrentUserId();

            UserRoleDto userRole = await userRoleRepository.GetByUserId(userId);

            if (userRole == null)
                throw new BadRequestException("User does not have a role");

            return userRole;
        }

        public async Task RemoveRole(long userRoleId)
        {
            UserRoleDto userRole = await userRoleRepository.GetById(userRoleId);

            if (userRole == null)
                throw new BadRequestException("User does not have a role");

            await userRoleRepository.Remove(userRoleId);
        }
    }
}
