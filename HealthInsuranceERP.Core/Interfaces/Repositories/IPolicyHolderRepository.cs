
using HealthInsuranceERP.Core.Dtos;
using HealthInsuranceERP.Core.DTOs;
using HealthInsuranceERP.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInsuranceERP.Core.Interfaces.Repositories
{
    public interface IPolicyHolderRepository
    {
        Task<PolicyholderDto[]> GetPolicyholder(string nationalID);
        Task<IEnumerable<PolicyholderDto>> GetAllPolicyholders();
        Task<PolicyholderDto> CreatePolicyholder(PolicyholderDto policyholder);
        Task<PolicyholderDto> UpdatePolicyholder(PolicyholderDto policyholder);
        Task<int> DeletePolicyholder(int Id);

    }
}
