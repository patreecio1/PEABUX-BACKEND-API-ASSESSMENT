
using Azure.Core;
using HealthInsuranceERP.Core.Dtos;
using HealthInsuranceERP.Core.DTOs;
using HealthInsuranceERP.Core.Interfaces.Repositories;
using HealthInsuranceERP.Core.Utilities;
using HealthInsuranceERP.Data.Data;
using HealthInsuranceERP.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace HealthInsuranceERP.Data.Repositories
{ 
public class ClaimRepository : IClaimRepository
{
    private readonly ApplicationDbContext _dbContext;
    public ClaimRepository(ApplicationDbContext aPPContext)
    {

        _dbContext = aPPContext;

    }
    public async Task<ClaimDto[]> GetClaims()
    {
        var request = await _dbContext.Claims.ToListAsync();
        if (request.Count==0)
            return Array.Empty<ClaimDto>();
        var result = request.Select(r => new ClaimDto().Assign(r)).ToArray();
        return result;
    }
        public async Task<ClaimDto> CreateClaim(ClaimDto claimDto)
        {
            var claimEntity = new Claim();
            claimEntity.CreatedBy = "Admin";
            claimEntity.ModifiedBy = "Admin";
            claimEntity.ClaimId = GenerateRandomClaimId();
            claimEntity.ClaimStatus = "Submitted";
            claimEntity.NationalId = claimDto.NationalId;

            double totalAmount = 0;
            foreach (var expenseDto in claimDto.Expenses)
            {
                var expenseEntity = new Expense
                {
                    Name = expenseDto.Name,
                    Amount = expenseDto.Amount,
                    DateOfExpense = expenseDto.DateOfExpense,
                    ClaimId =  claimEntity.ClaimId,

                };
                _dbContext.Expenses.Add(expenseEntity);
                totalAmount += expenseDto.Amount; // Add the expense amount to the total
            }
            // Calculate the total amount for the ClaimDto
            var claimDtoWithTotalAmount = new ClaimDto().Assign(claimEntity);
               claimDtoWithTotalAmount.TotalAmount = totalAmount; // Assign the total amount to the ClaimDto
               claimDtoWithTotalAmount.Expenses = claimDto.Expenses; 

            _dbContext.Claims.Add(claimEntity);

            // Save changes to the database
            await _dbContext.SaveChangesAsync();

            return claimDtoWithTotalAmount;
        }

        public int GenerateRandomClaimId()
        {
            // Create a random number generator.
            Random random = new Random();

            // Generate a random 6-digit number between 100,000 and 999,999.
            int claimId = random.Next(100000, 999999);

            return claimId;
        }

        public async Task<ClaimDto> ApproveClaim(int Id, string Message)
        {
            var fetchClaims = await _dbContext.Claims.Where(x => x.Id == Id).FirstOrDefaultAsync();
            if (fetchClaims != null)
            {
                fetchClaims.ApprovalMessage = Message;
                fetchClaims.ClaimStatus = "Approved";
                _dbContext.Claims.Update(fetchClaims);
                _dbContext.SaveChanges();

                // Create a ClaimDto and populate it with data
                ClaimDto claimDto = new ClaimDto
                {
                    // Populate fields based on your data model
                  ApprovalMessage  = fetchClaims.ApprovalMessage,
                  ClaimStatus = fetchClaims.ClaimStatus,
                  NationalId = fetchClaims.NationalId,
                //  TotalAmount = fetchClaims.to
                };

                return claimDto;
            }

            // Return null or handle the case when fetchClaims is null
            return null;
        }

        public async Task<ClaimDto> RejectClaim(int Id, string Message)
        {
            var fetchClaims = await _dbContext.Claims.Where(x => x.Id == Id).FirstOrDefaultAsync();
            if (fetchClaims != null)
            {
                fetchClaims.ApprovalMessage = Message;
                fetchClaims.ClaimStatus = "Rejected";
                _dbContext.Claims.Update(fetchClaims);
                _dbContext.SaveChanges();

                // Create a ClaimDto and populate it with data
                ClaimDto claimDto = new ClaimDto
                {
                    // Populate fields based on your data model
                    ApprovalMessage  = fetchClaims.ApprovalMessage,
                    ClaimStatus = fetchClaims.ClaimStatus,
                    NationalId = fetchClaims.NationalId,
                  
                };

                return claimDto;
            }

            // Return null or handle the case when fetchClaims is null
            return null;
        }

    }
}
