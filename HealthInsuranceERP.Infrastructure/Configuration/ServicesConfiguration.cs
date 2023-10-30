
using HealthInsuranceERP.Core.Dtos;
using HealthInsuranceERP.Core.Interfaces.Managers;
using HealthInsuranceERP.Core.Interfaces.Repositories;
using HealthInsuranceERP.Core.Interfaces.Services;
using HealthInsuranceERP.Core.Managers;
using HealthInsuranceERP.Core.Models;
using HealthInsuranceERP.Data.Data;
using HealthInsuranceERP.Data.Repositories;
using HealthInsuranceERP.Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HealthInsuranceERP.Infrastructure.Configuration
{
    public static class ServicesConfiguration
    {
      
        public static void AppServicesConfiguration(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddHttpClient();
            services.AddHttpClient<IHttpService>("http", c => { })
                .ConfigurePrimaryHttpMessageHandler(h =>
                {

                    var handler = new HttpClientHandler();
                    handler.ClientCertificateOptions = ClientCertificateOption.Manual;
                    handler.ServerCertificateCustomValidationCallback = (httpRequestMessage, cert, cetChain, policyErrors) => { return true; };
                    return handler;
                });
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


            services.AddScoped<IDapperContext, DapperContext>();
            //services
            services.AddScoped(typeof(ILoggerService<>), typeof(LoggerService<>));
            services.AddSingleton<IHttpService, HttpService>();
            services.AddSingleton<IHttpContextService, HttpContextService>();
            services.AddScoped<IUtilityService, UtilityService>();
            services.AddScoped<IEmailService, EmailService>();

            services.AddScoped<IJWTService, JWTService>();

            //Manager
            services.AddScoped<IClaimManager, ClaimManager>();
            services.AddScoped<IRoleManager, RoleManager>();
            services.AddScoped<IPermissionManager, PermissionManager>();
            services.AddScoped<IRolePermissionManager, RolePermissionManager>();
            services.AddScoped<IUserRoleManager, UserRoleManager>();
            services.AddScoped<IAccountManager, AccountManager>();
            services.AddScoped<IUserManager, UserManager>();
            services.AddScoped<IPolicyHolderManager, PolicyHolderManager>();


            //Repository
            services.AddScoped<IClaimRepository, ClaimRepository>();

            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IPermissionRepository, PermissionRepository>();
            services.AddScoped<IRolePermissionRepository, RolePermissionRepository>();
            services.AddScoped<IUserRoleRepository, UserRoleRepository>();
            services.AddScoped<IEmailLogRepository, EmailLogRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IConfirmationTokenRepository, ConfirmationTokenRepository>();
            services.AddScoped<IPasswordHistoryRepository, PasswordHistoryRepository>();
            services.AddScoped<ILoginAttemptLogRepository, LoginAttemptLogRepository>();
            services.AddScoped<IPolicyHolderRepository, PolicyHolderRepository>();
        }
    }
}

