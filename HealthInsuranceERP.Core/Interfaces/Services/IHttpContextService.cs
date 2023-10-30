using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInsuranceERP.Core.Interfaces.Services
{
    public interface IHttpContextService
    {
        string CurrentUsername();
        string CurrentUserId();
        //  Task<string> ClientToken();
        // Dictionary<string, string> GetHeader();
        string GetCalerIP();
    }
}
