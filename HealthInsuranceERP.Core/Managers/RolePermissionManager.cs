
using HealthInsuranceERP.Core.Dtos;
using HealthInsuranceERP.Core.DTOs;
using HealthInsuranceERP.Core.Exceptions;
using HealthInsuranceERP.Core.Interfaces.Managers;
using HealthInsuranceERP.Core.Interfaces.Repositories;
using HealthInsuranceERP.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInsuranceERP.Core.Managers
{
    public class RolePermissionManager : IRolePermissionManager
    {
        private readonly IRoleRepository roleRepository;
        private readonly IPermissionRepository permissionRepository;
        private readonly IRolePermissionRepository rolePermissionRepository;
        public RolePermissionManager(IRolePermissionRepository rolePermissionRepository,
            IPermissionRepository permissionRepository, IRoleRepository roleRepository)
        {
            this.roleRepository = roleRepository;
            this.permissionRepository = permissionRepository;
            this.rolePermissionRepository = rolePermissionRepository;
        }

        public async Task<RolePermissionDto> Activate(long id)
        {
            RolePermissionDto rolePermissionDto = await rolePermissionRepository.GetById(id);

            if (rolePermissionDto == null)
                throw new BadRequestException("Record does not exist");

            rolePermissionDto = await rolePermissionRepository.Activate(id);
            if (rolePermissionDto == null)
                throw new BadRequestException("Request faied. Kindly retry");

            return rolePermissionDto;
        }

        public async Task<int> AddRolePermissions(long roleId, RolePermissionModel rolePermissionRequest)
        {
            return await rolePermissionRepository.AddRolePermissions(roleId, rolePermissionRequest);
        }

        public async Task<RolePermissionDto> Create(RolePermissionDto result)
        {
            RoleDto role = await roleRepository.GetById(result.RoleId);
            if (role == null)
                throw new BadRequestException("Role does not exist");

            PermissionDto permission = await permissionRepository.GetById(result.PermissionId);
            if (permission == null)
                throw new BadRequestException("Permission doesnot exist");

            RolePermissionDto rolePerm = await rolePermissionRepository.Create(result);
            if (rolePerm == null)
                throw new BadRequestException("Request failed. Kindly retry");

            return rolePerm;
        }

        public async Task<RolePermissionDto> Deactivate(long id)
        {
            RolePermissionDto rolePermissionDto = await rolePermissionRepository.GetById(id);

            if (rolePermissionDto == null)
                throw new BadRequestException("Record does not exist");

            rolePermissionDto = await rolePermissionRepository.Activate(id);
            if (rolePermissionDto == null)
                throw new BadRequestException("Request faied. Kindly retry");

            return rolePermissionDto;
        }

        public async Task<RolePermissionDto[]> GetPermissionsforRoleId(long roleId)
        {
            return await rolePermissionRepository.GetPermissionsforRoleId(roleId);
        }

        public async Task<Page<RolePermissionDto>> GetRolePermissionByRoleId(long roleId, int pageNumber, int pageSize)
        {
            return await rolePermissionRepository.GetByRoleId(roleId, pageNumber, pageSize);
        }

        public async Task<PermissionDto[]> GetUnAddedPermission(long roleId)
        {
            PermissionDto[] existPerm = await rolePermissionRepository.GetPermissions(roleId);
            PermissionDto[] alPerm = await permissionRepository.GetPermissions();

            if (existPerm.Length <= 0)
                return alPerm;

            return alPerm.Where(s => existPerm.Any(a => a.PermissionName != s.PermissionName)).ToArray();
        }

        public async Task<RolePermissionDto> Update(RolePermissionDto result)
        {
            RolePermissionDto model = await rolePermissionRepository.GetById(result.Id);
            if (model == null)
                throw new BadRequestException("Request does not exist");

            model = await rolePermissionRepository.Update(result);
            if (model == null)
                throw new BadRequestException("Request failed. Kindly retry.");

            return model;
        }
    }
}
