
using HealthInsuranceERP.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInsuranceERP.Core.Dtos
{
    public class AppUserDto : BaseModel<string>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Position { get; set; }
        public string EmployeeId { get; set; }
        public bool IsSuperAdmin { get; set; }
        public string Password { get; set; }
        public long LocationId { get; set; }
        public string LocationName { get; set; }
        public long FunctionId { get; set; }
        public string FunctionName { get; set; }
        public long UnitId { get; set; }
        public string UnitName { get; set; }

        public string? ManagerId { get; set; }

        public int LockoutCounter { get; set; }
        public bool IsLockedOut { get; set; }
        public DateTime? DateAccountLocked { get; set; }
        public bool MustChangePassword { get; set; }
        public DateTime? LastLogin { get; set; }

        public DateTime? ReleasedDate { get; set; }
        public String Message { get; set; }

        public Boolean IsAway { get; set; }
        public DateTime? AwayStartDate { get; set; }
        public DateTime? AwayEndate { get; set; }
        public string? DOA { get; set; }
        public string? DOASetBy { get; set; }
        public long DelegationId { get; set; }

     

        public long[] RoleIds { get; set; }

    }
}
