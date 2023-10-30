using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using HealthInsuranceERP.Data.Data;
using HealthInsuranceERP.Core.Interfaces.Services;
using HealthInsuranceERP.Core.Dtos;
using HealthInsuranceERP.Data.Entities;
using HealthInsuranceERP.Core.Models;
using HealthInsuranceERP.Core.Utilities;
using HealthInsuranceERP.Core.DTOs;
using HealthInsuranceERP.Core.Exceptions;
using HealthInsuranceERP.Core.Interfaces.Repositories;
using HealthInsuranceERP.Data.Utilities;

namespace HealthInsuranceERP.Data.Repositories
{
    public class RolePermissionRepository : IRolePermissionRepository
    {
        private readonly IHttpContextService httpContextService;

        private readonly ApplicationDbContext dbContext;
        private readonly ILoggerService<RolePermissionRepository> _loggerService;
        public RolePermissionRepository(ApplicationDbContext appContext,
            IHttpContextService httpContextService, ILoggerService<RolePermissionRepository> loggerService)
        {

            this.httpContextService = httpContextService;
            dbContext = appContext;
            this._loggerService = loggerService;
        }

        public async Task<RolePermissionDto> Create(RolePermissionDto result)
        {
            RolePermission rolePermission = new RolePermission().Assign(result);
            rolePermission.CreatedBy = httpContextService.CurrentUsername();

            dbContext.RolePermissions.Add(rolePermission);
            await dbContext.SaveChangesAsync();

            return await GetById(rolePermission.Id);
        }

        public async Task<RolePermissionDto> Update(RolePermissionDto result)
        {
            RolePermission entity = await dbContext.RolePermissions
                .FirstOrDefaultAsync(a => a.Id == result.Id);

            if (entity == null)
                return null;

            entity.CanAdd = result.CanAdd;
            entity.CanDelete = result.CanDelete;
            entity.CanEdit = result.CanEdit;
            entity.CanRead = result.CanRead;
            entity.PermissionId = result.PermissionId;
            entity.RoleId = result.RoleId;
            entity.CanApprove = result.CanApprove;
            entity.CanTerminate = result.CanTerminate;

            await dbContext.SaveChangesAsync();

            return await GetById(entity.Id);
        }

        public async Task<RolePermissionDto> Activate(long id)
        {
            var entity = await dbContext.RolePermissions
               .Include(s => s.Role)
               .Include(s => s.Permission)
               .FirstOrDefaultAsync(a => a.Id == id);

            if (entity == null)
                return null;
            if (entity.IsActive)
                return ToEntity(entity);

            entity.IsActive = true;
            entity.ModifiedBy = httpContextService.CurrentUsername();
            entity.ModifiedDate = DateTimeOffset.Now;

            await dbContext.SaveChangesAsync();

            return ToEntity(entity);
        }

        public async Task<RolePermissionDto> GetById(long id)
        {
            var entity = await dbContext.RolePermissions
               .Include(s => s.Role)
               .Include(s => s.Permission)
               .FirstOrDefaultAsync(a => a.Id == id);

            if (entity == null)
                return null;

            return ToEntity(entity);
        }

        public async Task<Page<RolePermissionDto>> GetByRoleId(long roleId, int pageNumber, int pageSize)
        {
            var e = dbContext.RolePermissions
                .Include(a => a.Role)
                .Include(a => a.Permission)
                .Where(a => a.RoleId == roleId)
                .OrderBy(a => a.Permission.PermissionName);

            RolePermissionDto[] perm = Array.Empty<RolePermissionDto>();
            var result = await e.ToPageListAsync(pageNumber, pageSize);
            if (result.Data.Length > 0)
                perm = result.Data.Select(x =>
                {
                    //  var e = mapper.Map<RolePermissionDto>(x);
                    var e = new RolePermissionDto().Assign(x);
                    //  e.Permission = mapper.Map<PermissionDto>(x.Permission);
                    e.Permission = new PermissionDto().Assign(x.Permission);
                    // e.Role = mapper.Map<RoleDto>(x.Role);
                    e.Role = new RoleDto().Assign(x.Role);
                    return e;
                }).ToArray();

            return new Page<RolePermissionDto>(perm, result.PageCount, pageNumber, pageSize);
        }
        //  var response = result.Select(r => new EmailLogDto().Assign(r)).ToArray();
        public async Task<RolePermissionDto[]> GetPermissionsforRoleId(long roleId)
        {
            var e = dbContext.RolePermissions
                .Include(a => a.Role)
                .Include(a => a.Permission)
                .Where(a => a.RoleId == roleId)
                .OrderBy(a => a.Permission.PermissionName);

            RolePermissionDto[] perm = Array.Empty<RolePermissionDto>();
            var result = await e.ToPageListAsync(1, 100);
            if (result.Data.Length > 0)
                perm = result.Data.Select(x =>
                {
                    var e = new RolePermissionDto().Assign(x);
                    //e.Permission = mapper.Map<PermissionDto>(x.Permission);
                    // e.Role = mapper.Map<RoleDto>(x.Role);

                    e.RoleName = e.Role.RoleName;
                    e.PermissionName = e.Permission.PermissionName;
                    e.Role = null;
                    e.Permission = null;
                    return e;
                }).ToArray();

            return perm;
        }

        public async Task<PermissionDto[]> GetPermissions(long roleId)
        {
            var e = await dbContext.RolePermissions
               .Where(a => a.RoleId == roleId)
               .Select(a => a.Permission)
               .OrderBy(a => a.PermissionName).ToArrayAsync();

            if (e.Length <= 0)
                return Array.Empty<PermissionDto>();

            var response = e.Select(r => new PermissionDto().Assign(r)).ToArray();
            return response;// mapper.Map<PermissionDto[]>(e);
        }

        private RolePermissionDto ToEntity(RolePermission entity)
        {
            if (entity == null)
                return null;

            RolePermissionDto model = new RolePermissionDto().Assign(entity);

            if (entity.Permission != null)
                model.Permission = new PermissionDto().Assign(entity.Permission);
            if (entity.Role != null)
                model.Role = new RoleDto().Assign(entity.Role);

            return model;
        }

        public async Task<int> AddRolePermissions(long roleId, RolePermissionModel rolePermissionRequest)
        {



            var permsId = dbContext.Set<Permission>().Select(x => x.Id).ToArray();

            var rolePerms = (from p in rolePermissionRequest.Permissions
                             where (permsId.Contains(p.PermissionId))
                             select new RolePermission
                             {
                                 CanApprove = p.CanApprove,
                                 CanAdd = p.CanCreate,
                                 CanRead = p.CanRead,
                                 CanDelete = p.CanDelete,
                                 CanEdit = p.CanUpdate,
                                 CanTerminate = p.CanTerminate,
                                 PermissionId = p.PermissionId,
                                 IsActive = true,
                                 CreatedDate = DateTimeOffset.Now,
                                 RoleId = roleId,
                                 CreatedBy = this.httpContextService.CurrentUsername()





                             }).ToArray();





            if (rolePerms.Length <= 0)
            {
                return 0;
            }
            var distincts = rolePerms.Where(p => p.PermissionId != null).GroupBy(p => p.PermissionId).Select(grp => grp.FirstOrDefault()).ToArray();


            try
            {
                dbContext.Set<RolePermission>().AddRange(distincts);
                int count = await dbContext.SaveChangesAsync();
                return count;
            }
            catch (Exception ex)
            {
                _loggerService.LogError(ex);
                throw new BadRequestException(ex.InnerException.Message);


            }


        }

        public async Task<RolePermissionDto[]> GetPermissionsforSuperAdmin()
        {
            var e = dbContext.Permissions
                   .OrderBy(a => a.PermissionName).ToList();
            var roles = dbContext.Roles
                .OrderBy(a => a.RoleName).ToList();


            List<RolePermissionDto> perm = new List<RolePermissionDto>();

            foreach (var p in e)
            {
                foreach (var role in roles)
                {
                    perm.Add(new RolePermissionDto { CanAdd = true, CanDelete = true, CanEdit = true, CanRead = true, PermissionId = p.Id, RoleId = role.Id, PermissionName = p.PermissionName });


                }


            }
            return perm.ToArray();
        }

    }
}
