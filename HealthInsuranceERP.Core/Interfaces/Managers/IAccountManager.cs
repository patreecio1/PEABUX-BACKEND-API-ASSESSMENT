using HealthInsuranceERP.Core.Dtos;
using HealthInsuranceERP.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInsuranceERP.Core.Interfaces.Managers
{
    public interface IAccountManager
    {
        Task<bool> ConfirmationEmail(string password, string token);
        Task<bool> ResendConfirmationAccount(string email);

        Task<bool> ResetPassword(string password, string token);

        Task<bool> SendForgetPassword(string email);
        Task<LoginResponseModel> Login(string email, string password);

        Task<AppUserDto> SyncSuperAdmin();
        Task<int> UpdatePassword(string NewPassword, string oldPassword, bool mustchangepasswor);
        Task<int> ReleaseLockedAccounts();
        Task<LoginResponseModel> PasswordlessSignIn(string userinfo);
    }
}
