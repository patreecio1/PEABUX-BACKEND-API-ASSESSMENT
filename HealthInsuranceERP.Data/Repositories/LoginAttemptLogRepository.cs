
using HealthInsuranceERP.Core.Dtos;
using HealthInsuranceERP.Core.Interfaces.Repositories;
using HealthInsuranceERP.Core.Utilities;
using HealthInsuranceERP.Data.Data;
using HealthInsuranceERP.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInsuranceERP.Data.Repositories
{
    public class LoginAttemptLogRepository : ILoginAttemptLogRepository
    {
      
        private readonly ApplicationDbContext aPPContext;
        public LoginAttemptLogRepository(ApplicationDbContext aPPContext
               )
        {
     
            this.aPPContext = aPPContext;
        }

        public async Task<int> CountLast1mins(string email)
        {
            DateTimeOffset dat = DateTimeOffset.Now.AddMinutes(-1);

            return await aPPContext.LoginAttemptLogs.Where(x => x.CreatedDate > dat && x.Email == email).CountAsync();
        }

        public async Task<LoginAttemptLogDto> Create(LoginAttemptLogDto model)
        {
            LoginAttemptLog log = new LoginAttemptLog().Assign(model);

            aPPContext.LoginAttemptLogs.Add(log);
            await aPPContext.SaveChangesAsync();

            return new LoginAttemptLogDto().Assign(log);
        }
    }
}
