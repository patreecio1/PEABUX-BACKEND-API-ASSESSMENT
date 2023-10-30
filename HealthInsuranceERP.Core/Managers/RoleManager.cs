

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
    public class RoleManager : IRoleManager
    {
        private readonly IRoleRepository roleRepository;
        private readonly IUserRoleRepository userRoleRepository;
        private readonly IHttpContextService httpContextService;
        public RoleManager(IRoleRepository roleRepository, IUserRoleRepository userRoleRepository)
        {
            this.roleRepository = roleRepository;
            this.httpContextService = httpContextService;
            this.userRoleRepository = userRoleRepository;
        }

        public async Task<RoleDto> Activate(long id)
        {
            RoleDto role = await roleRepository.GetById(id);
            if (role == null)
                throw new BadRequestException("Role does not exist");

            role = await roleRepository.Activate(id);

            if (role == null)
                throw new BadRequestException("Request failed. Kindly retry");

            return role;
        }

        public async Task<RoleDto> Create(RoleDto model)
        {
            RoleDto role = await roleRepository.GetByRoleName(model.RoleName);
            if (role != null)
                throw new BadRequestException("Role already exist");

            role = await roleRepository.Create(model);
            if (role == null)
                throw new BadRequestException("Request failed. Kindly retry");

            return role;
        }

        public async Task<RoleDto> Deactivate(long id)
        {
            RoleDto role = await roleRepository.GetById(id);
            if (role == null)
                throw new BadRequestException("Role does not exist");

            role = await roleRepository.Deactivate(id);

            if (role == null)
                throw new BadRequestException("Request failed. Kindly retry");

            return role;
        }

        public async Task<UserRoleDto[]> GetCurrentUserRole()
        {
            string userId = httpContextService.CurrentUserId();
            if (string.IsNullOrEmpty(userId))
                throw new BadRequestException("user detais can not be retrieved");

            return await roleRepository.GetByUserId(userId);
        }

        public async Task<UserRoleDto[]> GetCurrentUserRole(string userId)
        {
            if (string.IsNullOrEmpty(userId))
                throw new BadRequestException("user detais can not be retrieved");

            // return await roleRepository.GetByUserId(userId);
            return await userRoleRepository.GetRolesforUser(userId);
        }

        public async Task<Page<RoleDto>> GetRolePaginated(int pageNumber, int pageSize)
        {
            return await roleRepository.GetPaginated(pageNumber, pageSize);
        }

        public async Task<RoleDto[]> GetRoles()
        {
            return await roleRepository.GetRoles();
        }

        public async Task<RoleDto> UpdateRole(RoleDto model)
        {
            RoleDto role = await roleRepository.GetById(model.Id);
            if (role == null)
                throw new BadRequestException("Role does not exist");

            role = await roleRepository.Update(model);

            if (role == null)
                throw new BadRequestException("Request fail. Kindly retry");

            return role;
        }
    }
}
