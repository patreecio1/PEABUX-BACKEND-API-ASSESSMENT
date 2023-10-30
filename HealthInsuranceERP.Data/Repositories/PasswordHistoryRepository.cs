using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthInsuranceERP.Data.Data;
using HealthInsuranceERP.Core.Interfaces.Services;
using HealthInsuranceERP.Core.DTOs;
using HealthInsuranceERP.Data.Entities;
using HealthInsuranceERP.Core.Utilities;
using HealthInsuranceERP.Core.Interfaces.Repositories;

namespace HealthInsuranceERP.Data.Repositories
{
    public class PasswordHistoryRepository: IPasswordHistoryRepository
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IHttpContextService httpContextService;
        private readonly IUtilityService utilityService;
        public PasswordHistoryRepository(ApplicationDbContext aPPContext, IHttpContextService httpContextService, IUtilityService utilityService)
        {
            this.dbContext = aPPContext;
            this.httpContextService = httpContextService;
            this.utilityService = utilityService;
        }

        public async Task<PasswordHistoryDto> Create(PasswordHistoryDto model)
        {

            var itemsToUpdate = await dbContext.PasswordHistories.Where(x => x.Email == model.Email).ToListAsync();
            foreach (var item in itemsToUpdate)
            {
                item.IsCurrentPassword = false;
            }
            await dbContext.SaveChangesAsync();

            var entity = new PasswordHistory().Assign(model);
            entity.Password = utilityService.ToSha256(model.Password);


            dbContext.PasswordHistories.Add(entity);
            await dbContext.SaveChangesAsync();
            return new PasswordHistoryDto().Assign(entity);

        }

        public async Task<PasswordHistoryDto> GetByUserId(string UserId, string password)
        {
            var hashedpassword = utilityService.ToSha256(password);
            var entity = await dbContext.Set<PasswordHistory>().Where(x => x.UserId == UserId && x.Password == hashedpassword).FirstOrDefaultAsync();
            if (entity == null)
                return null;
            else
                return new PasswordHistoryDto().Assign(entity);

        }
    }
}
