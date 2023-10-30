
using HealthInsuranceERP.Core.Dtos;
using HealthInsuranceERP.Core.DTOs;
using HealthInsuranceERP.Core.Interfaces.Repositories;
using HealthInsuranceERP.Core.Interfaces.Services;
using HealthInsuranceERP.Core.Utilities;
using HealthInsuranceERP.Data.Data;
using HealthInsuranceERP.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using static Dapper.SqlMapper;
using static HealthInsuranceERP.Data.Repositories.PolicyHolderRepository;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace HealthInsuranceERP.Data.Repositories
{ 
public class PolicyHolderRepository : IPolicyHolderRepository
    {
    private readonly ApplicationDbContext _dbContext;
    public PolicyHolderRepository(ApplicationDbContext aPPContext)
        {
        _dbContext = aPPContext;
         }
        public async Task<PolicyholderDto[]> GetPolicyholder(string nationalID)
        {

            var request = await _dbContext.PolicyHolders.Where(x=>x.NationalId == nationalID).ToArrayAsync();
            if (request == null)
                return Array.Empty<PolicyholderDto>();
            var result = request.Select(r => new PolicyholderDto().Assign(r)).ToArray();
            return result;
        }

        public async Task<IEnumerable<PolicyholderDto>> GetAllPolicyholders()
            {
            var request = await _dbContext.PolicyHolders.ToListAsync();
            if (request.Count==0)
                return Array.Empty<PolicyholderDto>();
            var result = request.Select(r => new PolicyholderDto().Assign(r)).ToArray();
            return result;
          
            }

            public async Task<PolicyholderDto> CreatePolicyholder(PolicyholderDto policyholder)
            {
            var entity = new Policyholder().Assign(policyholder);
            entity.CreatedBy = "Admin";
            entity.ModifiedBy = "Admin";

            // entity.CreatedBy = httpContextService.CurrentUsername();

            _dbContext.PolicyHolders.Add(entity);
            await _dbContext.SaveChangesAsync();

            return new PolicyholderDto().Assign(entity);
        }

            public async Task<PolicyholderDto> UpdatePolicyholder(PolicyholderDto policyholder)
            {
            Policyholder entity = await _dbContext.PolicyHolders.FindAsync(policyholder.Id);
            if (entity == null)
                return null;

            entity.CreatedBy = "Admin";
           
            await _dbContext.SaveChangesAsync();
            return new PolicyholderDto().Assign(entity);

        }

            public async Task<int>  DeletePolicyholder(int Id)
            {
            var e = await _dbContext.PolicyHolders.Where(x => x.Id ==Id).FirstAsync();
            if (e == null)
                return 0;

            _dbContext.Remove(e);

            return await _dbContext.SaveChangesAsync();
        }
  }

    }

