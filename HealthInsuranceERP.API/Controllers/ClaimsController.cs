using HealthInsuranceERP.API.RequestModels;
using HealthInsuranceERP.Core.Dtos;
using HealthInsuranceERP.Core.DTOs;
using HealthInsuranceERP.Core.Exceptions;
using HealthInsuranceERP.Core.Interfaces.Managers;
using HealthInsuranceERP.Core.Managers;
using HealthInsuranceERP.Core.Models;
using HealthInsuranceERP.Core.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HealthInsuranceERP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClaimsController : ControllerBase
    {

        private readonly IClaimManager _iclaimManager;

        public ClaimsController(IClaimManager iDriverManager)
        {
            this._iclaimManager = iDriverManager;
        }
        [AllowAnonymous]
        [Authorize]
        [HttpGet("GetClaims")]
        public async Task<IActionResult> GetClaims()
        {
            var result = await _iclaimManager.GetClaims();
            return Ok(new ResponseModel<object>
            {
                RequestSuccessful = true,
                ResponseCode = ResponseCodes.Successful,
                ResponseData = result
            });
        }

        [AllowAnonymous]
        [Authorize]
        [Route("CreateClaims")]

        // POST /api/policyholders
        [HttpPost]
        public async Task<IActionResult> CreateClaims([FromBody] CreateClaimVm createClaimVm)
        {
            createClaimVm.Validate();

            var claimDetails = new ClaimDto
            {
                NationalId = createClaimVm.NationalId,
                ClaimStatus = "Submitted", // Set the default value or use a value from createClaimVm
                                           // Map other properties from createClaimVm

                // Map the list of expenses
                Expenses = createClaimVm.Expenses.Select(expenseVm => new ExpenseDto
                {
                    Name = expenseVm.Name,
                    Amount = expenseVm.Amount,
                    DateOfExpense = expenseVm.DateOfExpense
                }).ToList()
            };

            var result = await _iclaimManager.CreateClaim(claimDetails);

            return Ok(new ResponseModel<object>
            {
                RequestSuccessful = true,
                ResponseCode = ResponseCodes.Successful,
                ResponseData = result
            });
        }

        [AllowAnonymous]
        [Authorize]
        [HttpPost("ApproveClaim")]
        public async Task<IActionResult> ApproveClaim([FromBody] ApproveVm model)
        {
            if (model.Id <= default(long))
            {
                throw new BadRequestException("Invalid Request Id");
            }
            var result = await _iclaimManager.ApproveClaim(model.Id, model.Message);

            return Ok(new ResponseModel<object>
            {
                RequestSuccessful = true,
                ResponseCode = ResponseCodes.Successful,
                ResponseData = result,
                Message = "Request approved successfully."
            });

        }
        [AllowAnonymous]
        [Authorize]
        [HttpPost("RejectClaim")]
        public async Task<IActionResult> RejectClaim([FromBody] ApproveVm model)
        {
            if (model.Id <= default(long))
            {
                throw new BadRequestException("Invalid Request Id");
            }
            var result = await _iclaimManager.RejectClaim(model.Id, model.Message);

            return Ok(new ResponseModel<object>
            {
                RequestSuccessful = true,
                ResponseCode = ResponseCodes.Successful,
                ResponseData = result,
                Message = "Request Rejected successfully."
            });

        }



    }
}
