using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInsuranceERP.Core.Enums
{
    public enum PermissionEnum
    {

        Role,
        User,
        Locations,
        Departments,
        Queries,
        Permissions,
        Reports,
        
    }

    public enum PermissionAction
    {
        Add, Update, Delete, Read
    }
}
