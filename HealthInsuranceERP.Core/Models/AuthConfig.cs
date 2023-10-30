using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace HealthInsuranceERP.Core.Models
{
    public class AuthConfig
    {

        public string Scope { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public bool RequiredHttpMetaData { get; set; }
        public string OtherScopes { get; set; }
        public int TokenExpiredTimeInMins { get; set; }
        public bool IsLive { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Key { get; set; }
    }
}

