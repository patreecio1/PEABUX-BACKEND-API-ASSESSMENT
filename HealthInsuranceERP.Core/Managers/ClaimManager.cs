
using HealthInsuranceERP.Core.Dtos;
using HealthInsuranceERP.Core.Interfaces.Managers;
using HealthInsuranceERP.Core.Interfaces.Repositories;

namespace HealthInsuranceERP.Core.Managers
{
    public class ClaimManager : IClaimManager
    {

        private readonly IClaimRepository _claimRepository;

        public ClaimManager(IClaimRepository claimRepository)
        {
            this._claimRepository = claimRepository;
        }

        public async Task<ClaimDto[]> GetClaims()
        {
            return await _claimRepository.GetClaims();
        }
        
        public async Task<ClaimDto> CreateClaim(ClaimDto policyclaim)
        {
            return await _claimRepository.CreateClaim(policyclaim);
        }
        
        public async Task<ClaimDto> ApproveClaim(int Id, string Message)
        {
            return await _claimRepository.ApproveClaim(Id, Message);
        }
        public async Task<ClaimDto> RejectClaim(int Id, string Message)
        {
            return await _claimRepository.RejectClaim(Id, Message);
        }
    }
}
