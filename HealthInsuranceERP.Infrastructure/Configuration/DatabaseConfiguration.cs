using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
//using HealthInsuranceERP.Core.Enums;
using HealthInsuranceERP.Core.Interfaces.Repositories;
using HealthInsuranceERP.Core.Interfaces.Services;
using HealthInsuranceERP.Data;
using HealthInsuranceERP.Data.Entities;
using System.Security;
using HealthInsuranceERP.Core.Enums;
using HealthInsuranceERP.Data.Data;
using Microsoft.Extensions.Hosting;

namespace HealthInsuranceERP.Infrastructure.Configuration
{
    public static class DatabaseConfiguration
    {
        public static void AddDatabaseConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddEntityFrameworkSqlServer()
           .AddDbContextPool<ApplicationDbContext>((serviceProvider, optionsBuilder) =>
           {
               optionsBuilder.UseSqlServer(connectionString);
               optionsBuilder.UseInternalServiceProvider(serviceProvider);
           });

        }
        public static IHost DbMigration(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var logger = scope.ServiceProvider.GetService<ILoggerService<IHost>>();
                try
                {
                    ApplicationDbContext dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                    dbContext.Database.Migrate();
                    SyncRoles(dbContext, logger);
                    SyncPermission(dbContext, logger);
                    InitializeSupperUser(scope);

                }
                catch (Exception ex)
                {
                    logger.LogError(ex);
                }
            }
            return host;
        }
        public static void InitializeSupperUser(IServiceScope scope)
        {
            var logger = scope.ServiceProvider.GetService<ILoggerService<IHost>>();
            try
            {
                IUserRepository userService = scope.ServiceProvider.GetRequiredService<IUserRepository>();
                var p = userService.CreateDefaultUser().Result;

                if (p != null)
                    logger.LogInformation($"Super user registration failed");
            }
            catch (Exception ex)
            {
                logger.LogError(ex);
            }
        }
       
        public static void SyncPermission(ApplicationDbContext dbContext, ILoggerService<IHost> loggerService)
        {
            try
            {
                string[] perms = Enum.GetNames<PermissionEnum>();
                string[] permissions = dbContext.Permissions.Select(x => x.PermissionName).ToArray();

                string[] result = perms.Except(permissions).ToArray();

                if (result.Length > 0)
                {
                    Permission[] entities = result.Select(x => new Permission
                    {
                        CreatedBy = "Application",
                        CreatedDate = DateTimeOffset.Now,
                        IsActive = true,
                        PermissionDescription = x,
                        PermissionDisplayName = x,
                        PermissionName = x
                    }).ToArray();

                    dbContext.Permissions.AddRange(entities);
                    dbContext.SaveChanges();


                }

            }
            catch (Exception ex)
            {
                loggerService.LogError(ex);
            }
        }
        public static void SyncRoles(ApplicationDbContext dbContext, ILoggerService<IHost> loggerService)
        {
            try
            {
                string[] theRoles = { "Admin", "PolicyHolder"};
                string[] functions = dbContext.Roles.Select(x => x.RoleName).ToArray();

                string[] result = theRoles.Except(functions).ToArray();

                if (result.Length > 0)
                {
                    Role[] entities = result.Select(x => new Role
                    {
                        CreatedBy = "Application",
                        CreatedDate = DateTimeOffset.Now,
                        IsActive = true,
                        RoleName = x,
                        RoleDescription = x,
                        IsDeleted = false
                    }).ToArray();

                    dbContext.Roles.AddRange(entities);
                    dbContext.SaveChanges();


                }

            }
            catch (Exception ex)
            {
                loggerService.LogError(ex);
            }
        }

    }
}
