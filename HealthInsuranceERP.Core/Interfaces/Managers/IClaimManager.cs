
using HealthInsuranceERP.Core.Dtos;
using HealthInsuranceERP.Core.DTOs;

namespace HealthInsuranceERP.Core.Interfaces.Managers
{
    public interface IClaimManager
    {
        // Claim GetById(int claimId);
        Task<ClaimDto[]> GetClaims();
        Task<ClaimDto> CreateClaim(ClaimDto policyclaim);
        Task<ClaimDto> ApproveClaim(int Id, string Message);
        Task<ClaimDto> RejectClaim(int Id, string Message);

    }
}
