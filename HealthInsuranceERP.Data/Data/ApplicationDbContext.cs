
using HealthInsuranceERP.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Security.Authentication.ExtendedProtection;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HealthInsuranceERP.Data.Data
{
  
        public class ApplicationDbContext : DbContext
        {
            public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
           : base(options)
            {
            }

            public ApplicationDbContext()
            {

            }
        public DbSet<AppUser> AppUsers { get; set; }
      
        public DbSet<EmailLog> EmailLogs { get; set; }
        public DbSet<LoginAttemptLog> LoginAttemptLogs { get; set; }

        public DbSet<Permission> Permissions { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<PasswordHistory> PasswordHistories { get; set; }
         public DbSet<ConfirmationToken> ConfirmationTokens { get; set; }
         public DbSet<Policyholder> PolicyHolders { get; set; }
        public DbSet<Claim> Claims { get; set; }
        public DbSet<Expense> Expenses { get; set; }

        }

    }

