

using HealthInsuranceERP.Core.Dtos;
using HealthInsuranceERP.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInsuranceERP.Core.Interfaces.Repositories
{
    public interface IClaimRepository
    {
      
        Task<ClaimDto[]> GetClaims();
        Task<ClaimDto> CreateClaim(ClaimDto policyholder);
        Task<ClaimDto> ApproveClaim(int Id, string Message);
        Task<ClaimDto> RejectClaim(int Id, string Message);
       // int GenerateRandomClaimId();

    }
}
