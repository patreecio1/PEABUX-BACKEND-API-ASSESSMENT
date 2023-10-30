using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;


namespace HealthInsuranceERP.Core.Interfaces.Repositories
{
    public interface IDapperContext
    {
        IDbConnection GetDbConnection();

    }
}
