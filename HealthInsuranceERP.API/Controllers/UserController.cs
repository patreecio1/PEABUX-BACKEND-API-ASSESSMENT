
using HealthInsuranceERP.API.RequestModels;
using HealthInsuranceERP.Core.Dtos;
using HealthInsuranceERP.Core.Interfaces.Managers;
using HealthInsuranceERP.Core.Models;
using HealthInsuranceERP.Core.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace HealthInsuranceERP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserManager _userManager;
        
        public UserController(IUserManager userManager)
        {
            
            _userManager = userManager;
        }
        [AllowAnonymous]
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserVm model)
        {
            model.Validate();

            AppUserDto user = new AppUserDto().Assign(model);
          
            user = await _userManager.CreateUser(user);

            return Ok(ResponseModel<AppUserDto>.IsSuccessful(user, "Confirmation email has been sent to user email address"));
        }
        [AllowAnonymous]
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 20)
        {
            Page<AppUserDto> users = await _userManager.GetUsers(pageNumber, pageSize);
            return Ok(users);
        }
        [AllowAnonymous]
        [Authorize]
        [HttpPut("{userId}")]
        public async Task<IActionResult> Update(string userId, [FromBody] UserVm model)
        {
            if (string.IsNullOrEmpty(userId))
                return BadRequest(ResponseModel<string>.Failed("", "user is required"));

            AppUserDto user = new AppUserDto().Assign(model);
            user.Id = userId;
            user = await _userManager.Update(user);

            return Ok(ResponseModel<AppUserDto>.IsSuccessful(user, "Successful"));
        }
        [AllowAnonymous]
        [Authorize]
        [HttpPut("activate/{userId}")]
        public async Task<IActionResult> Activate(string userId)
        {
            if (string.IsNullOrEmpty(userId))
                return BadRequest(ResponseModel<string>.Failed("", "user is required"));

            AppUserDto user = await _userManager.Activate(userId);

            return Ok(ResponseModel<AppUserDto>.IsSuccessful(user, "Successful"));
        }
        [AllowAnonymous]
        [Authorize]
        [HttpPut("deactivate/{userId}")]
        public async Task<IActionResult> Dectivate(string userId)
        {
            if (string.IsNullOrEmpty(userId))
                return BadRequest(ResponseModel<string>.Failed("", "user is required"));

            AppUserDto user = await _userManager.Deactivate(userId);

            return Ok(ResponseModel<AppUserDto>.IsSuccessful(user, "Successful"));
        }

        [AllowAnonymous]
        [Authorize]
        [HttpGet("getAllUsers")]
        public async Task<IActionResult> Get()
        {
            var users = await _userManager.GetAllUsers();
            return Ok(ResponseModel<AppUserDto[]>.IsSuccessful(users, "Successful"));

        }
        [AllowAnonymous]
        [Authorize]

        [HttpPost("assignRoleToUser")]
        public async Task<IActionResult> AssignRoleToUser([FromBody] UserRoleRequestVM model)
        {
            model.Validate();
            return Ok(new ResponseModel<object>
            {
                RequestSuccessful = true,
                ResponseCode = ResponseCodes.Successful,
                ResponseData = await _userManager.AssignRoletoUser(model.UserId, model.RoleId),
                Message = "Role added to user successfully"
            });
        }

        [AllowAnonymous]
        [Authorize]
        [HttpPost("assignRolesToUser")]
        public async Task<IActionResult> assignRolesToUser([FromBody] MultipleUserRoleRequestVM model)
        {
            model.Validate();
            return Ok(new ResponseModel<object>
            {
                RequestSuccessful = true,
                ResponseCode = ResponseCodes.Successful,
                ResponseData = await _userManager.AssignMultipleRolestoUser(model.UserId, model.RoleIds),
                Message = "Role added to user successfully"
            });
        }
     
    }
}
