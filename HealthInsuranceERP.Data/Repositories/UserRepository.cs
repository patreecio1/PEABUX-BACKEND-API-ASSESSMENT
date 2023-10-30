
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

using System.Diagnostics.Contracts;
using HealthInsuranceERP.Data.Data;
using HealthInsuranceERP.Core.Interfaces.Services;
using HealthInsuranceERP.Core.Interfaces.Repositories;
using HealthInsuranceERP.Core.Dtos;
using HealthInsuranceERP.Core.Utilities;
using HealthInsuranceERP.Data.Entities;
using HealthInsuranceERP.Core.Models;
using HealthInsuranceERP.Data.Utilities;
using HealthInsuranceERP.Core.DTOs;
using Dapper;

namespace HealthInsuranceERP.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IHttpContextService httpContextService;

        private readonly ApplicationDbContext _dbContext;
        private readonly IMemoryCache memoryCache;
        private readonly IDapperContext dapperContext;
        private readonly IUtilityService utilityService;
        private readonly ILoggerService<UserRepository> loggerService;
        private readonly IRoleRepository roleRepository;
        private readonly IUserRoleRepository userRoleRepository;

        public UserRepository(IUserRoleRepository userRoleRepository,

            IDapperContext dapperContext, IUtilityService utilityService,
             IHttpContextService httpContextService, IRoleRepository roleRepository, IMemoryCache memoryCache, ApplicationDbContext aPPContext, ILoggerService<UserRepository> loggerService)
        {
            this.memoryCache = memoryCache;
            this.httpContextService = httpContextService;

            this._dbContext = aPPContext;
            this.dapperContext = dapperContext;
            this.utilityService = utilityService;
            this.loggerService = loggerService;

            this.roleRepository = roleRepository;
            this.userRoleRepository = userRoleRepository;

        }
        public async Task<AppUserDto> Activate(string userId)
        {
            var entity = await _dbContext.AppUsers.FindAsync(userId);
            if (entity == null)
                return null;

            entity.IsActive = true;
            entity.ModifiedBy = httpContextService.CurrentUsername();
            entity.ModifiedDate = DateTimeOffset.Now;

            await _dbContext.SaveChangesAsync();

            return new AppUserDto().Assign(entity);
        }

        public async Task<AppUserDto> CreateUser(AppUserDto userDto)
        {
            // AppUser entity = _mapper.Map<AppUser>(userDto);
            var entity = new AppUser
            {
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Email = userDto.Email,
                PhoneNumber = userDto.PhoneNumber,
                MustChangePassword = true,
                Fullname =  userDto.LastName + ' ' +  userDto.FirstName,
                Message = "User Created Successfully",
                ModifiedBy = "Admin",

            };

            entity.Password = utilityService.ToSha256(userDto.Password);
            entity.Id = Guid.NewGuid().ToString();
            entity.CreatedBy = "Admin";
            _dbContext.AppUsers.Add(entity);

            await _dbContext.SaveChangesAsync();
            return new AppUserDto().Assign(entity);

        }

        public async Task<AppUserDto> Deactivate(string userId)
        {
            var entity = await _dbContext.AppUsers.FindAsync(userId);
            if (entity == null)
                return null;

            entity.IsActive = false;
            entity.ModifiedBy = httpContextService.CurrentUsername();
            entity.ModifiedDate = DateTimeOffset.Now;

            await _dbContext.SaveChangesAsync();

            return new AppUserDto().Assign(entity);
        }

        public async Task<AppUserDto> GetById(string userId)
        {
            AppUser entity = await _dbContext.AppUsers.FindAsync(userId);
            if (entity == null)
                return null;

            return new AppUserDto().Assign(entity);
        }


        public async Task<RolePermissionDto[]> GetCurrentUserPermission(string userId)
        {
            //  string key = $"permission{userId}";

            RolePermissionDto[] permissions = Array.Empty<RolePermissionDto>();

            //if (!memoryCache.TryGetValue<RolePermissionDto[]>(key, out permissions))
            //{

            string query = " select rp.*, p.PermissionName from UserRoles ur " +
                " inner join RolePermissions rp on rp.RoleId = ur.RoleId  " +
                " inner join Permissions p on p.Id = rp.PermissionId " +
                " where ur.UserId = @userId ";


            using (var connection = dapperContext.GetDbConnection())
            {
                var result = await connection.QueryAsync<RolePermissionDto>(query, new { @userId = userId });
                if (result.Count() > 0)
                {
                    permissions = result.Select(r => new RolePermissionDto().Assign(r)).ToArray();

                }
                //       memoryCache.Set(key, permissions, DateTimeOffset.Now.AddMinutes(3));
            }
            //   }

            return permissions;
        }
        public async Task<AppUserDto> GetSuperAdmin()
        {
            AppUser entity = await _dbContext.AppUsers.FirstOrDefaultAsync(a => a.IsSuperAdmin);
            if (entity == null)
                return null;

            return new AppUserDto().Assign(entity);


        }

        public async Task<AppUserDto> GetUserByEmail(string email)
        {

            //AppUser entity = await _dbContext.AppUsers.FirstOrDefaultAsync(a => a.Email == email);
            //if (entity == null)
            //    return null;

            //return _mapper.Map<AppUserDto>(entity);
            var result = await (from appUser in _dbContext.Set<AppUser>()

                                where appUser.Email == email

                                select new AppUserDto
                                {

                                    Email = appUser.Email,
                                    PhoneNumber = appUser.PhoneNumber,
                                    LastName = appUser.LastName,
                                    FirstName = appUser.FirstName,
                                    IsSuperAdmin = appUser.IsSuperAdmin,
                                    Id = appUser.Id,
                                    MustChangePassword = appUser.MustChangePassword,
                                    LockoutCounter = appUser.LockoutCounter,
                                    IsLockedOut = appUser.IsLockedOut,
                                    LastLogin = appUser.LastLogin,
                                    DateAccountLocked = appUser.DateAccountLocked,
                                    IsAway = appUser.IsAway,
                                    AwayEndate = appUser.AwayEndate,
                                    AwayStartDate = appUser.AwayStartDate,



                                }).FirstOrDefaultAsync();



            return result;

        }
        public async Task<AppUserDto> Login(string email, string password)
        {

            var result = await (from appUser in _dbContext.Set<AppUser>()

                                where appUser.Email == email && appUser.Password == utilityService.ToSha256(password)

                                select new AppUserDto
                                {
                                    //   RoleId = role.Id,
                                    // RoleName = role.RoleName,
                                    Email = appUser.Email,
                                    PhoneNumber = appUser.PhoneNumber,
                                    LastName = appUser.LastName,
                                    FirstName = appUser.FirstName,
                                    IsSuperAdmin = appUser.IsSuperAdmin,
                                    Id = appUser.Id,
                                    MustChangePassword = appUser.MustChangePassword,
                                    LockoutCounter = appUser.LockoutCounter,
                                    IsLockedOut = appUser.IsLockedOut,
                                    LastLogin = appUser.LastLogin,
                                    IsAway = appUser.IsAway,
                                    AwayEndate = appUser.AwayEndate,
                                    AwayStartDate = appUser.AwayStartDate,


                                }).FirstOrDefaultAsync();


            if (result == null)
            {
                return null;
            }

            else
            {

                return result;

            }


        }


        public async Task<Page<AppUserDto>> GetUsers(int pageNumber, int pageSize)
        {
            var query = _dbContext.AppUsers.OrderBy(a => a.LastName);

            AppUserDto[] appUserDto = Array.Empty<AppUserDto>();
            var result = await query.ToPageListAsync<AppUser>(pageNumber, pageSize);

            if (result.Data.Length > 0)

                appUserDto = result.Data.Select(r => new AppUserDto().Assign(r)).ToArray();

            return new Page<AppUserDto>(appUserDto, result.PageCount, pageNumber, pageSize);
        }

        public async Task<AppUserDto> Update(AppUserDto model)
        {
            AppUser entity = await _dbContext.AppUsers.FindAsync(model.Id);
            if (entity == null)
                return null;

            entity.LastName = model.LastName;
            entity.FirstName = model.FirstName;
            entity.PhoneNumber = model.PhoneNumber;

            await _dbContext.SaveChangesAsync();
            return new AppUserDto().Assign(entity);
        }

        public Task<AppUserDto> UpdateConfirmation(string email)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateIsSuperAdmin(string id)
        {
            throw new NotImplementedException();
        }

        public Task<PermissionDto[]> GetCurrentUserPermission()
        {
            throw new NotImplementedException();
        }

        public async Task<int> UpdatePassword(string email, string NewPassword, bool MustChangePassword)
        {

            AppUser appUser = await _dbContext.AppUsers.FirstOrDefaultAsync(a => a.Email == email);
            appUser.Password = utilityService.ToSha256(NewPassword);
            appUser.ModifiedBy = httpContextService.CurrentUsername();
            appUser.ModifiedDate = DateTimeOffset.Now;
            appUser.MustChangePassword = MustChangePassword;
            appUser.IsLockedOut = false;
            appUser.LockoutCounter = 0;

            var result = await _dbContext.SaveChangesAsync();

            return result;
        }

      
        public async Task<AppUserDto> CreateDefaultUser()
        {
            AppUserDto user = await GetSuperAdmin();
            if (user != null)
            {
                loggerService.LogInformation("Super admin already exist");
                return user;
            }


            var role = await roleRepository.GetByRoleName("Admin");
            if (role == null)
            { role = await roleRepository.Create(new RoleDto { RoleName = "Admin", RoleDescription = "Admin" }); }


            user = new AppUserDto { FirstName = "Super", LastName = "Admin", Email = "superadmin@yopmail.com", PhoneNumber = "08187470737", Password = "admin123", IsSuperAdmin = true };

            //  return await CreateUser(user);

            var entity = new AppUser().Assign(user);

            entity.Password = utilityService.ToSha256(user.Password);
            entity.Id = Guid.NewGuid().ToString();
            entity.CreatedBy = "Application";// httpContextService.CurrentUsername();
            _dbContext.AppUsers.Add(entity);

            await _dbContext.SaveChangesAsync();

            return new AppUserDto().Assign(entity);

        }


        public async Task<AppUserDto[]> GetAllUsers()
        {
            var result = await (from appUser in _dbContext.Set<AppUser>()

                                select new AppUserDto
                                {
                                    //  RoleId = role.Id,
                                    //    RoleName = role.RoleName,
                                    Email = appUser.Email,
                                    PhoneNumber = appUser.PhoneNumber,
                                    LastName = appUser.LastName,
                                    FirstName = appUser.FirstName,
                                    IsSuperAdmin = appUser.IsSuperAdmin,

                                    Id = appUser.Id,
                                    IsAway = appUser.IsAway,
                                    AwayEndate = appUser.AwayEndate,
                                    AwayStartDate = appUser.AwayStartDate,


                                }).ToArrayAsync();



            return result;
        }




        public async Task<AppUserDto> UpdateLockCounter(string email)
        {
            var entity = await _dbContext.AppUsers.Where(x => x.Email == email).FirstOrDefaultAsync();
            if (entity != null && entity.LockoutCounter != 3)
            { entity.LockoutCounter++; }

            if (entity.LockoutCounter == 3)
            {
                entity.IsLockedOut = true;
                entity.DateAccountLocked = DateTime.Now;


            }

            entity.ModifiedBy = httpContextService.CurrentUsername();
            entity.ModifiedDate = DateTimeOffset.Now;

            await _dbContext.SaveChangesAsync();


            return new AppUserDto().Assign(entity);
        }

        public async Task<int> ReleaseLockedAccounts()
        {

            var itemsToUpdate = await _dbContext.AppUsers.Where(x => x.LockoutCounter == 3 && x.IsLockedOut == true && (DateTime.Now > x.DateAccountLocked.AddHours(1))).ToListAsync();

            if (itemsToUpdate.Count() > 0)
            {
                foreach (var item in itemsToUpdate)
                {
                    item.LockoutCounter = 0;
                    item.IsLockedOut = false;
                    item.ReleasedDate = DateTime.Now;
                    item.Message = $"Account Unlocked at {DateTime.Now}";
                }
            }


            var result = await _dbContext.SaveChangesAsync();

            return result;
        }

        public async Task<AppUserDto> UpdateLastLogin(string email)
        {
            var entity = await _dbContext.AppUsers.Where(x => x.Email == email).FirstOrDefaultAsync();

            if (entity != null)
            {
                entity.LastLogin = DateTime.Now;

            }

            // entity.ModifiedBy = httpContextService.CurrentUsername();
            entity.ModifiedBy ="Admin";
            entity.ModifiedDate = DateTimeOffset.Now;

            await _dbContext.SaveChangesAsync();
            return new AppUserDto().Assign(entity);
        }

        public async Task<AppUserDto[]> GetByIds(string[] Ids)
        {


            var result = (from appUser in _dbContext.Set<AppUser>()
                          where Ids.Contains(appUser.Id)


                          select new AppUserDto
                          {
                              Email = appUser.Email,
                              PhoneNumber = appUser.PhoneNumber,
                              LastName = appUser.LastName,
                              FirstName = appUser.FirstName,
                              IsSuperAdmin = appUser.IsSuperAdmin,
                              Id = appUser.Id,
                              MustChangePassword = appUser.MustChangePassword,
                              LockoutCounter = appUser.LockoutCounter,
                              IsLockedOut = appUser.IsLockedOut,
                              LastLogin = appUser.LastLogin,
                              DateAccountLocked = appUser.DateAccountLocked,
                              IsAway = appUser.IsAway,
                              AwayEndate = appUser.AwayEndate,
                              AwayStartDate = appUser.AwayStartDate,



                          }).AsEnumerable().OrderBy(x => x.LastName).ToArray();


            if (result.Count() <= 0)
                return Array.Empty<AppUserDto>();

            return result;

        }

    }
}