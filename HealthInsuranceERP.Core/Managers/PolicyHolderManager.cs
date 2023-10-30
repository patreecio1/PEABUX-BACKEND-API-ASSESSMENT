
using HealthInsuranceERP.Core.Dtos;
using HealthInsuranceERP.Core.DTOs;
using HealthInsuranceERP.Core.Interfaces.Managers;
using HealthInsuranceERP.Core.Interfaces.Repositories;

namespace HealthInsuranceERP.Core.Managers
{
    public class PolicyHolderManager : IPolicyHolderManager
    {

        private readonly IPolicyHolderRepository _policyholder;

        public PolicyHolderManager(IPolicyHolderRepository policyHolderRepository)
        {
            this._policyholder = policyHolderRepository;
        }
       
        public async Task<PolicyholderDto[]> GetPolicyholder(string nationalID)
        {
            return await _policyholder.GetPolicyholder(nationalID);
        }


        public async Task<IEnumerable<PolicyholderDto>> GetAllPolicyholders()
        {
            return await _policyholder.GetAllPolicyholders();
        }


        public async Task<PolicyholderDto> CreatePolicyholder(PolicyholderDto policyholder)
        {
            return await _policyholder.CreatePolicyholder(policyholder);
        }


        public async Task<PolicyholderDto> UpdatePolicyholder(PolicyholderDto policyholder)
        {
            return await _policyholder.UpdatePolicyholder(policyholder);
        }

        public async Task<int> DeletePolicyholder(int Id)
        {
            return await _policyholder.DeletePolicyholder(Id);
        }

    }
}
