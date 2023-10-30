

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
    public class UserManager : IUserManager
    {

        private readonly IUserRepository _userRepository;
        private readonly IConfirmationTokenRepository _confirmationTokenRepository;
        private readonly AppUrl _appUrl;
        private readonly IHttpContextService _httpContextService;
        private readonly IEmailService _emailService;
        private readonly IUserRoleRepository userRoleRepository;
        private readonly IRoleRepository roleRepository;


        public UserManager(IRoleRepository roleRepository, IUserRoleRepository userRoleRepository, IUserRepository userRepository, IConfirmationTokenRepository confirmationTokenRepository,
            IHttpContextService httpContextService, AppUrl appUrl, IEmailService emailService, IConfirmationTokenRepository confirmationTokenRepository1)
        {

            _userRepository = userRepository;
            this._confirmationTokenRepository = confirmationTokenRepository;
            _appUrl = appUrl;
            _httpContextService = httpContextService;
            _emailService = emailService;

            this.userRoleRepository = userRoleRepository;
            this.roleRepository =   roleRepository;

        }

        public async Task<AppUserDto> Activate(string userId)
        {
            AppUserDto user = await _userRepository.GetById(userId);
            if (user == null)
                throw new BadRequestException("User does not exist"); ;


            user = await _userRepository.Activate(userId);
            if (user == null)

                throw new BadRequestException("Request failed. Kindly retry");
            return user;
        }

        public async Task<AppUserDto> CreateUser(AppUserDto model)
        {
            AppUserDto userDto = await _userRepository.GetUserByEmail(model.Email);
            if (userDto != null)
                throw new BadRequestException("Email already exist");

            model.Password = CryptographyExtensions.CreatePassword(6);
            var appUser = await _userRepository.CreateUser(model);
            if (appUser == null)
                throw new BadRequestException("User registration failed. Kindly retry or contact administrator if error persist");


            // Insert User Roles
            var rest = await userRoleRepository.Create(appUser.Id, model.RoleIds);


            // send account confirmation link
            await NewClientUserCreationEmail(model.Email, model.Password);

            return appUser;
        }


        public async Task<AppUserDto> Deactivate(string userId)
        {
            AppUserDto user = await _userRepository.GetById(userId);
            if (user == null)
                throw new BadRequestException("User does not exist"); ;


            user = await _userRepository.Deactivate(userId);
            if (user == null)

                throw new BadRequestException("Request failed. Kindly retry");
            return user;
        }

        public async Task<Page<AppUserDto>> GetUsers(int pageNumber, int pageSize)
        {
            return await _userRepository.GetUsers(pageNumber, pageSize);
        }

        public async Task<AppUserDto> Update(AppUserDto model)
        {
            AppUserDto user = await _userRepository.GetById(model.Id);
            if (user == null)
                throw new BadRequestException("User does not exist");

            user = await _userRepository.Update(model);
            if (user == null)
                throw new BadRequestException("Request failed. Kindly retry");

            return user;
        }

        public async Task<string> SendAccountConfirmation(string email)
        {
            var user = await _userRepository.GetUserByEmail(email);
            if (user == null)
                throw new NotFoundException($"{email} does not exist");


            var entity = await CreateToken(user.Id);
            if (entity == null)
                throw new BadRequestException("Request failed");

            var baseUrl = _appUrl.ConfirmtaionUrlLink;
            string username = $"{user.LastName} {user.FirstName}";

            string comfirmationLink = $"{baseUrl}/#/confirm_email/?token={entity.TokenId}";
            var filecontent = System.IO.File.ReadAllText("EmailTemplate/ConfirmationEmail.html");

            filecontent = filecontent.Replace("##NAME##", username);


            //filecontent = filecontent.Replace("##ACTIVATIONLINK##", comfirmationLink);
            //await _emailService.Send(new EmailRequestModel
            //{

            //    Body = filecontent,
            //    Subject = "Confirm Account Registration",
            //    DestinationEmail = new Dictionary<string, string>() { { email, username } },

            //});
            //   await _emailService.SendMail("Confirm Account Registration", email, filecontent);


            return "Please follow the link sent to your email for account confirmation";
        }

        public async Task<string> NewClientUserCreationEmail(string email, string password)
        {
            var user = await _userRepository.GetUserByEmail(email);
            if (user == null)
                throw new NotFoundException($"{email} does not exist");



            string loginURL = _appUrl.Url1;
            string username = $"{user.LastName} {user.FirstName}";
            string LOGINURL2 = _appUrl.Url2;




            var filecontent = System.IO.File.ReadAllText("EmailTemplate/ClientUserCreation.html");

            filecontent = filecontent.Replace("##NAME##", $"{username}");
            filecontent = filecontent.Replace("##LOGINNAME##", $"{email}");
            filecontent = filecontent.Replace("##LOGINPASSWORD##", password);
            filecontent = filecontent.Replace("##LOGINURL##", loginURL);
            filecontent = filecontent.Replace("##LOGINURL2##", LOGINURL2);
            //await _emailService.Send(new EmailRequestModel
            //{

            //    Body = filecontent,
            //    Subject = "Account Creation Notification",
            //    DestinationEmail = new Dictionary<string, string>() { { email, username } },

            //});



            return "Please follow the link sent to your email for account confirmation";
        }



        public async Task<ConfirmationTokenDto> CreateToken(string userId)
        {
            var model = new ConfirmationTokenDto
            {
                TokenId = Guid.NewGuid().ToString().Replace("-", string.Empty).ToLower(),
                Token = Guid.NewGuid().ToString().Replace("-", string.Empty).ToLower(),
                UserId = userId,
                ExpiredDate = DateTime.Now.AddMinutes(15),
                CreatedBy = _httpContextService.CurrentUserId()

            };

            return await _confirmationTokenRepository.Create(model);
        }

        public async Task<AppUserDto[]> GetAllUsers()
        {
            return await _userRepository.GetAllUsers();
        }


        public async Task<UserRoleDto> AssignRoletoUser(string userId, long roleId)
        {
            var result = await userRoleRepository.Create(new UserRoleDto { UserId = userId, RoleId = roleId });
            return result;
        }
        public async Task<int> AssignMultipleRolestoUser(string userId, long[] roleId)
        {
            var result = await userRoleRepository.Create(userId, roleId);
            return result;
        }
    }
}
