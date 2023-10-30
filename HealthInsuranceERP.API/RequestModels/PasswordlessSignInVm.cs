using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthInsuranceERP.API.RequestModels
{
    public class PasswordlessSignInVm : Model
    {
        public string email { get; set; }
    }
}
