using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace HealthInsuranceERP.Core.Models
{
    public class RolePermissionModel
    {

        [Required(ErrorMessage = "Role Id is required")]

        public long RoleId { get; set; }

        [MinLength(1, ErrorMessage = "Permission is required")]
        public RolePermModel[] Permissions { get; set; }
    }
    public class RolePermModel
    {
        public long PermissionId { get; set; }
        public bool CanRead { get; set; }
        public bool CanUpdate { get; set; }
        public bool CanDelete { get; set; }
        public bool CanCreate { get; set; }
        public bool CanApprove { get; set; }
        public bool CanTerminate { get; set; }


    }
}

 
