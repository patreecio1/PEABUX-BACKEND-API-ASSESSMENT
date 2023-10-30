
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HealthInsuranceERP.Data.Data;
using HealthInsuranceERP.Core.DTOs;
using HealthInsuranceERP.Core.Utilities;
using HealthInsuranceERP.Core.Dtos;
using HealthInsuranceERP.Core.Interfaces.Repositories;

namespace HealthInsuranceERP.Data.Repositories
{
    public class PermissionRepository : IPermissionRepository
    {

        private readonly ApplicationDbContext aPPContext;
        public PermissionRepository(ApplicationDbContext appContext)
        {

            this.aPPContext = appContext;
        }

        public async Task<PermissionDto> GetById(long permissionId)
        {
            var result = await aPPContext.Permissions.FindAsync(permissionId);
            if (result == null)
                return null;

            return new PermissionDto().Assign(result);
        }

        public async Task<PermissionDto[]> GetPermissions()
        {
            var result = await aPPContext.Permissions.OrderBy(x => x.PermissionName).ToArrayAsync();
            if (result.Length <= 0)
                return Array.Empty<PermissionDto>();


            var response = result.Select(r => new PermissionDto().Assign(r)).ToArray();
            return response;//;.Map<PermissionDto[]>(result);
        }
        public async Task<RolePermissionDto[]> GetSuperuserPermissions()
        {
            var result = await aPPContext.Permissions.OrderBy(x => x.PermissionName).ToArrayAsync();
            if (result.Length <= 0)
                return Array.Empty<RolePermissionDto>();

            return result.Select(a => new RolePermissionDto
            {
                CanAdd = true,
                CanDelete = true,
                CanEdit = true,
                CanRead = true,
                PermissionName = a.PermissionName,
                PermissionId = a.Id
            }).ToArray();
        }
    }
}
