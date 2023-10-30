
using HealthInsuranceERP.Core.Dtos;
using HealthInsuranceERP.Core.DTOs;

namespace HealthInsuranceERP.Core.Interfaces.Managers
{
    public interface IPolicyHolderManager
    {
        Task<PolicyholderDto[]> GetPolicyholder(string nationalID);
        Task<IEnumerable<PolicyholderDto>> GetAllPolicyholders();
        Task<PolicyholderDto> CreatePolicyholder(PolicyholderDto policyholder);
        Task<PolicyholderDto> UpdatePolicyholder(PolicyholderDto policyholder);
        Task<int> DeletePolicyholder(int Id);
    }
}
