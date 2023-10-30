
using HealthInsuranceERP.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace HealthInsuranceERP.Core.Interfaces.Repositories
{
    public interface   IPasswordHistoryRepository
    {
        Task<PasswordHistoryDto> GetByUserId(string UserId, string password);

        Task<PasswordHistoryDto> Create(PasswordHistoryDto model);
    }
}
