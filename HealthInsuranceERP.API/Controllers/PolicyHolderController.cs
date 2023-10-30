using HealthInsuranceERP.API.RequestModels;
using HealthInsuranceERP.Core.DTOs;
using HealthInsuranceERP.Core.Interfaces.Managers;
using HealthInsuranceERP.Core.Models;
using HealthInsuranceERP.Core.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HealthInsuranceERP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PolicyHolderController : ControllerBase
    {

        private readonly IPolicyHolderManager _ipolicyHolderManager;

        public PolicyHolderController(IPolicyHolderManager policyHolderManager)
        {
            this._ipolicyHolderManager = policyHolderManager;
        }

        [AllowAnonymous]
        [Authorize]
        [Route("CreatePolicyholder")]
     
            // POST /api/policyholders
            [HttpPost]
            public async Task<IActionResult> CreatePolicyholder([FromBody] CreatePolicyHolderVm policyholder)
            {
            policyholder.Validate();

            var policyholderDetails = new PolicyholderDto().Assign(policyholder);

            var result = await _ipolicyHolderManager.CreatePolicyholder(policyholderDetails);
            return Ok(new ResponseModel<object>
            {
                RequestSuccessful = true,
                ResponseCode = ResponseCodes.Successful,
                ResponseData = result
            });
        }

        [AllowAnonymous]
        [Authorize]
        [HttpGet("GetPolicyholder")]
        public async Task<IActionResult> GetPolicyholder(string nationalID)
        {
            var result = await _ipolicyHolderManager.GetPolicyholder(nationalID);
            return Ok(new ResponseModel<object>
            {
                RequestSuccessful = true,
                ResponseCode = ResponseCodes.Successful,
                ResponseData = result
            });
        }
        [AllowAnonymous]
        [Authorize]
        [HttpGet("GetAllPolicyholders")]
        public async Task<IActionResult> GetAllPolicyholders()
        {
            var result = await _ipolicyHolderManager.GetAllPolicyholders();
            return Ok(new ResponseModel<object>
            {
                RequestSuccessful = true,
                ResponseCode = ResponseCodes.Successful,
                ResponseData = result
            });
        }
        [AllowAnonymous]
        [Authorize]
        [HttpPost("UpdatePolicyholder")]
        public async Task<IActionResult> UpdatePolicyholder(PolicyholderDto policyholder)
        {
            var result = await _ipolicyHolderManager.UpdatePolicyholder(policyholder);
            return Ok(new ResponseModel<object>
            {
                RequestSuccessful = true,
                ResponseCode = ResponseCodes.Successful,
                ResponseData = result
            });
        }
        [AllowAnonymous]
        [Authorize]
        [HttpPost("DeletePolicyholder")]
        public async Task<IActionResult> DeletePolicyholder(int Id)
        {
            var result = await _ipolicyHolderManager.DeletePolicyholder(Id);
            return Ok(new ResponseModel<object>
            {
                RequestSuccessful = true,
                ResponseCode = ResponseCodes.Successful,
                ResponseData = result
            });
        }

    }
      
    }

