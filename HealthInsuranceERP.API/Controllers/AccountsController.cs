using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System.Text.RegularExpressions;
using HealthInsuranceERP.Core.Models;
using HealthInsuranceERP.Core.Utilities;
using HealthInsuranceERP.Core.Exceptions;
using HealthInsuranceERP.Core.Interfaces.Managers;
using HealthInsuranceERP.API.RequestModels;

namespace HealthInsuranceERP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountManager _accountManager;
        public AccountsController(IAccountManager accountManager)
        {
            this._accountManager = accountManager;

        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginVm model)
        {
            model.Validate();
            LoginResponseModel result = await _accountManager.Login(model.Email, model.Password);
            return Ok(ResponseModel<LoginResponseModel>.IsSuccessful(result, "Successful"));
        }
        [HttpPost("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequestVm model)
        {
            model.Validate();

            var result = await _accountManager.SendForgetPassword(model.Email);
            return Ok(new ResponseModel<object>
            {
                RequestSuccessful = true,
                ResponseCode = ResponseCodes.Successful,
                Message = "Login information has been sent succesfully",
                ResponseData = result
            });
        }

        [Authorize]
        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordVm model)
        {
            model.Validate();
            var r = new Regex(@"(?=^.{6,}$)(?=.*\d)(?=.*[!@#$%^&*]+)(?![.\n])(?=.*[A-Z])(?=.*[a-z]).*$");
            var checkPassword = r.IsMatch(model.NewPassword);


            if (!checkPassword)
            {
                throw new BadRequestException("Password should be a minimum of 8 characters with a number, an upper case letter, a lower case and a special character");
            }
            var result = await _accountManager.UpdatePassword(model.NewPassword, model.CurrentPassword, false);
            return Ok(new ResponseModel<object>
            {
                RequestSuccessful = true,
                ResponseCode = ResponseCodes.Successful,
                Message = result > 0 ? "Password  Changed succesfully" : "Password change failed",
                ResponseData = "Successful"
            });
        }
        [HttpPost("PasswordlessSignIn")]
        public async Task<IActionResult> PasswordlessSignIn([FromBody] PasswordlessSignInVm model)
        {
            model.Validate();


            if (string.IsNullOrEmpty(model.email))
            {
                throw new BadRequestException("Invalid principal");
            }
            var result = await _accountManager.PasswordlessSignIn(model.email);
            return Ok(new ResponseModel<object>
            {
                RequestSuccessful = true,
                ResponseCode = ResponseCodes.Successful,
                Message = "Login succesful",
                ResponseData = result
            });
        }
    }
}
