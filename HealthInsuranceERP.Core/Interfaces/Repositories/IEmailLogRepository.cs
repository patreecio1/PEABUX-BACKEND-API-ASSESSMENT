
using HealthInsuranceERP.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace HealthInsuranceERP.Core.Interfaces.Repositories
{
    public interface IEmailLogRepository
    {
        Task<EmailLogDto> GetById(long id);
        Task<EmailLogDto[]> GetByRecipient(string Recipient);
        Task<EmailLogDto> Create(EmailLogDto model);
        Task<EmailLogDto[]> GetAllEmails();
    }
}
