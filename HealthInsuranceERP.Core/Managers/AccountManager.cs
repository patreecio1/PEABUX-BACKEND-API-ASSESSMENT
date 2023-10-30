
using HealthInsuranceERP.Core.Dtos;
using HealthInsuranceERP.Core.DTOs;
using HealthInsuranceERP.Core.Exceptions;
using HealthInsuranceERP.Core.Interfaces.Managers;
using HealthInsuranceERP.Core.Interfaces.Repositories;
using HealthInsuranceERP.Core.Interfaces.Services;
using HealthInsuranceERP.Core.Models;
using HealthInsuranceERP.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInsuranceERP.Core.Managers
{
    public class AccountManager : IAccountManager
    {
        private readonly IUserRepository _userRepository;
        private readonly ILoginAttemptLogRepository loginAttemptLogRepository;
        private readonly IPermissionRepository permissionRepository;
        private readonly IJWTService iJWTService;
        private readonly AppUrl _appUrl;
        private readonly IEmailService _emailService;
        private readonly IHttpContextService httpContextService;
        private readonly IUtilityService utilityService;
        private readonly IPasswordHistoryRepository passwordHistoryRepository;
        private readonly IUserRoleRepository userRoleRepository;


        private readonly IRolePermissionRepository rolePermissionRepository;
        public AccountManager(IUserRoleRepository userRoleRepository, IPasswordHistoryRepository passwordHistoryRepository, IUtilityService utilityService, IHttpContextService httpContextService, IUserRepository userRepository, AppUrl appUrl, ILoginAttemptLogRepository loginAttemptLogRepository,
            IPermissionRepository permissionRepository, IEmailService emailService, IJWTService jWTService, IRolePermissionRepository rolePermissionRepository)
        {
            _userRepository = userRepository;
            this.loginAttemptLogRepository = loginAttemptLogRepository;
            this.permissionRepository = permissionRepository;
            this.iJWTService = jWTService;
            this._appUrl = appUrl;
            this._emailService = emailService;
            this.rolePermissionRepository = rolePermissionRepository;
            this.httpContextService = httpContextService;
            this.utilityService = utilityService;
            this.passwordHistoryRepository = passwordHistoryRepository;
            this.userRoleRepository = userRoleRepository;


        }

        public Task<bool> ConfirmationEmail(string password, string token)
        {
            throw new NotImplementedException();
        }

        public async Task<LoginResponseModel> Login(string email, string password)
        {

            AppUserDto user = await _userRepository.GetUserByEmail(email);
            if (user == null)
                throw new BadRequestException("Email/password is invalid");


            var appuserDto = await _userRepository.Login(email, password);

            if (appuserDto != null)
            {
                if (appuserDto.IsLockedOut)
                {
                    throw new BadRequestException("Account is locked out. Please contact Admin");
                }
            }
            if (appuserDto == null)
            { //
                // update failed counter;
                var res = await _userRepository.UpdateLockCounter(email);
                if (res.IsLockedOut)
                {
                    throw new BadRequestException("Account is locked out. Please contact Admin");
                }
                else
                    throw new BadRequestException("Invalid Email/password");

            }


            if (!appuserDto.IsLockedOut)
            {

                var upd = await _userRepository.UpdateLastLogin(email);


            }

            var login = new LoginResponseModel();
            login.Profile = user;
            if (user.IsSuperAdmin)
                login.Permission = await permissionRepository.GetSuperuserPermissions();
            else
                login.Permission = await _userRepository.GetCurrentUserPermission(user.Id);

            var roles = await userRoleRepository.GetRolesforUser(user.Id);

            login.Roles = roles;

            login.AccessToken = iJWTService.GenerateToken(appuserDto);
            login.Profile.Password = null;
            //  login.Permission =login.Profile.IsSuperAdmin ? await rolePermissionRepository.GetPermissionsforSuperAdmin(): await _userRepository.GetCurrentUserPermission();
            return login;
        }

        public async Task<int> ReleaseLockedAccounts()
        {
            var res = await _userRepository.ReleaseLockedAccounts();
            return 1;
        }

        public Task<bool> ResendConfirmationAccount(string email)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ResetPassword(string password, string token)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> SendForgetPassword(string email)
        {
            if (string.IsNullOrEmpty(email) || !email.IsValidEmail())
            {
                throw new BadRequestException("Email is required");
            }
            AppUserDto user = await _userRepository.GetUserByEmail(email);
            if (user == null)
                throw new BadRequestException("Email/ password is invalid");

            var newPassword = CryptographyExtensions.CreatePassword(6);
            var result = _userRepository.UpdatePassword(email, newPassword, true);

            // now send mail to user


            string loginURL = _appUrl.Url1;
            string username = $"{user.LastName} {user.FirstName}";
            string LOGINURL2 = _appUrl.Url2;



            //var filecontent = System.IO.File.ReadAllText("EmailTemplates/ForgotPassword.html");

            //filecontent = filecontent.Replace("##NAME##", $"{username}");
            //filecontent = filecontent.Replace("##LOGINNAME##", $"{email}");
            //filecontent = filecontent.Replace("##LOGINPASSWORD##", newPassword);
            //filecontent = filecontent.Replace("##LOGINURL##", loginURL);
            //filecontent = filecontent.Replace("##LOGINURL2##", LOGINURL2);
            //await _emailService.Send(new EmailRequestModel
            //{

            //    Body = filecontent,
            //    Subject = "Password Recovery",
            //    DestinationEmail = new Dictionary<string, string>() { { email, username } },

            //});



            return true;

        }

        public Task<AppUserDto> SyncSuperAdmin()
        {
            throw new NotImplementedException();
        }

        public async Task<int> UpdatePassword(string NewPassword, string oldPassword, bool mustchangepasswor)
        {
            var UserId = httpContextService.CurrentUserId();
            var user = await _userRepository.GetById(UserId);
            var oldP = utilityService.ToSha256(oldPassword);
            if (oldP != user.Password)
                throw new BadRequestException("Old Password is not correct");


            var res = await passwordHistoryRepository.Create(new PasswordHistoryDto
            {
                UserId = user.Id,
                Email = user.Email,
                Password = NewPassword,
                IsCurrentPassword = true
            });

            var result = await _userRepository.UpdatePassword(user.Email, NewPassword, false);
            return result;

        }

        public async Task<LoginResponseModel> PasswordlessSignIn(string userinfo)
        {
            var user = await _userRepository.GetUserByEmail(userinfo);



            var login = new LoginResponseModel();
            login.Profile = user;
            if (user.IsSuperAdmin)
                login.Permission = await permissionRepository.GetSuperuserPermissions();
            else
                login.Permission = await _userRepository.GetCurrentUserPermission(user.Id);

            var roles = await userRoleRepository.GetRolesforUser(user.Id);

            login.Roles = roles;

            login.AccessToken = iJWTService.GenerateToken(user);
            login.Profile.Password = null;

            await _userRepository.UpdateLastLogin(user.Email);

            return login;

        }

    }
}
