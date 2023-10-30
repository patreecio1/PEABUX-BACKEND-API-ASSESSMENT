
using HealthInsuranceERP.Core.DTOs;
using HealthInsuranceERP.Core.Interfaces.Repositories;
using HealthInsuranceERP.Core.Utilities;
using HealthInsuranceERP.Data.Data;
using HealthInsuranceERP.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace HealthInsuranceERP.Data.Repositories
{
    public class ConfirmationTokenRepository : IConfirmationTokenRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ConfirmationTokenRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;

        }

        public async Task<ConfirmationTokenDto> Create(ConfirmationTokenDto model)
        {
            var entity = new ConfirmationToken
            {
                CreatedBy = model.CreatedBy,
                CreatedDate = model.CreatedDate,
                ModifiedBy = model.ModifiedBy,
                ModifiedDate = model.ModifiedDate,
                TokenId = model.TokenId,
                Token = CryptographyExtensions.Encrypt(model.Token),
                UserId = model.UserId,
                ExpiredDate = model.ExpiredDate




            };// model.Map();


            _dbContext.Set<ConfirmationToken>().Add(entity);
            await _dbContext.SaveChangesAsync();
            var dto = new ConfirmationTokenDto().Assign(entity);
            return dto;
        }

        public async Task<bool> Deactivate(string TokenId)
        {
            var entity = await _dbContext.Set<ConfirmationToken>().FirstOrDefaultAsync(x => x.TokenId == TokenId);
            if (entity == null)
                return false;

            entity.IsDeleted = true;
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<ConfirmationTokenDto> Get(string TokenId)
        {
            var entity = await _dbContext.Set<ConfirmationToken>()
                .Select(x => new ConfirmationToken
                {
                    Token = CryptographyExtensions.Decrypt(x.Token),
                    Id = x.Id,
                    TokenId = x.TokenId,
                    UserId = x.UserId,
                    ExpiredDate = x.ExpiredDate,
                    IsActive = x.IsActive,
                    IsDeleted = x.IsDeleted,
                    CreatedBy = x.CreatedBy,
                    CreatedDate = x.CreatedDate,
                    ModifiedBy = x.ModifiedBy,
                    ModifiedDate = x.ModifiedDate,


                })
                .FirstOrDefaultAsync(x => x.TokenId == TokenId);
            if (entity == null)
                return null;
            var dto = new ConfirmationTokenDto().Assign(entity);
            return dto;
        }

        public async Task<ConfirmationTokenDto> GetByToken(string token)
        {

            var entity = await _dbContext.Set<ConfirmationToken>().Select(x => new ConfirmationToken
            {
                Token = CryptographyExtensions.Decrypt(x.Token),
                Id = x.Id,
                TokenId = x.TokenId,
                UserId = x.UserId,
                ExpiredDate = x.ExpiredDate,
                IsActive = x.IsActive,
                IsDeleted = x.IsDeleted,
                CreatedBy = x.CreatedBy,
                CreatedDate = x.CreatedDate,
                ModifiedBy = x.ModifiedBy,
                ModifiedDate = x.ModifiedDate,


            }).FirstOrDefaultAsync(x => CryptographyExtensions.Decrypt(x.Token) == token);
            if (entity == null)
                return null;

            var dto = new ConfirmationTokenDto().Assign(entity);
            return dto;

        }
    }


}
