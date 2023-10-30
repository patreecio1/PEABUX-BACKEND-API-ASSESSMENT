
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthInsuranceERP.Data.Data;
using HealthInsuranceERP.Core.Interfaces.Repositories;
using HealthInsuranceERP.Core.Interfaces.Services;
using HealthInsuranceERP.Core.Dtos;
using HealthInsuranceERP.Core.Utilities;
using HealthInsuranceERP.Data.Entities;
using HealthInsuranceERP.Data.Utilities;
using HealthInsuranceERP.Core.Models;

namespace HealthInsuranceERP.Data.Repositories
{
    public class RoleRepository : IRoleRepository
    {

        private readonly ApplicationDbContext dbContext;
        private readonly IHttpContextService httpContextService;
        private readonly IUtilityService utilityService;
        public RoleRepository(ApplicationDbContext aPPContext, IUtilityService utilityService,
            IHttpContextService httpContextService)
        {

            this.dbContext = aPPContext;
            this.httpContextService = httpContextService;
            this.utilityService = utilityService;

        }

        public async Task<RoleDto> Activate(long id)
        {
            var entity = await dbContext.Roles.FindAsync(id);
            if (entity == null)
                return null;
            if (entity.IsActive)
                return new RoleDto().Assign(entity);

            entity.IsActive = true;
            entity.ModifiedBy = httpContextService.CurrentUsername();
            entity.ModifiedDate = DateTimeOffset.Now;

            await dbContext.SaveChangesAsync();

            return new RoleDto().Assign(entity);
        }

        public async Task<RoleDto> Create(RoleDto model)
        {
            Role entity = new Role().Assign(model);
            entity.RoleNameHash = utilityService.ToSha256(model.RoleName);
            entity.CreatedBy = httpContextService.CurrentUsername();

            dbContext.Roles.Add(entity);
            await dbContext.SaveChangesAsync();

            return new RoleDto().Assign(entity);
        }

        public async Task<RoleDto> Deactivate(long id)
        {
            var entity = await dbContext.Roles.FindAsync(id);
            if (entity == null)
                return null;
            if (!entity.IsActive)
                return new RoleDto().Assign(entity);

            entity.IsActive = false;
            entity.ModifiedBy = httpContextService.CurrentUsername();
            entity.ModifiedDate = DateTimeOffset.Now;

            await dbContext.SaveChangesAsync();
            return new RoleDto().Assign(entity);
        }

        public async Task<RoleDto> GetById(long id)
        {
            var entity = await dbContext.Roles.FindAsync(id);
            if (entity == null)
                return null;

            return new RoleDto().Assign(entity);
        }
        public async Task<RoleDto> GetByRoleName(string roleName)
        {
            string rolehash = utilityService.ToSha256(roleName);

            var entity = await dbContext.Roles.FirstOrDefaultAsync(a => a.RoleName == roleName);
            if (entity == null)
                return null;

            return new RoleDto().Assign(entity);
        }

        public async Task<UserRoleDto[]> GetByUserId(string userId)
        {
            UserRole[] result = await dbContext.UserRoles
                .Include(s => s.Role)
                .Where(x => x.UserId == userId).ToArrayAsync();

            if (result.Length <= 0)
                return Array.Empty<UserRoleDto>();
            var response = result.Select(r => new UserRoleDto().Assign(r)).ToArray();
            return response;//[]>(result);
        }

        public async Task<Page<RoleDto>> GetPaginated(int pageNumber, int pageSize)
        {
            var query = dbContext.Roles.OrderBy(x => x.RoleName);

            RoleDto[] roles = Array.Empty<RoleDto>();
            var result = await query.ToPageListAsync(pageNumber, pageSize);
            if (result.Data.Length > 0)
                // roles = mapper.Map<RoleDto[]>(result.Data);
                roles = result.Data.Select(r => new RoleDto().Assign(r)).ToArray();
            return new Page<RoleDto>(roles, result.PageCount, pageNumber, pageSize);
        }

        public async Task<RoleDto[]> GetRoles()
        {
            Role[] result = await dbContext.Roles
                              .Where(x => x.IsActive).ToArrayAsync();

            if (result.Length <= 0)
                return Array.Empty<RoleDto>();

            return result.Select(r => new RoleDto().Assign(r)).ToArray();
        }

        public async Task<RoleDto> Update(RoleDto model)
        {
            var entity = await dbContext.Roles.FindAsync(model.Id);
            if (entity == null)
                return null;

            entity.RoleName = model.RoleName;
            entity.RoleNameHash = utilityService.ToSha256(model.RoleName);
            entity.RoleDescription = model.RoleDescription;

            await dbContext.SaveChangesAsync();
            return new RoleDto().Assign(entity);
        }
    }
}
