
using HealthInsuranceERP.Core.Dtos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace HealthInsuranceERP.Core.Models
{
    public class LoginResponseModel
    {
        public LoginResponseModel()
        {
        }
        public string TokenType { get; set; }

        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
        [JsonProperty("expires_in")]
        public int ExpiredIn { get; set; }
        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }

        public AppUserDto Profile { get; set; }
        public RolePermissionDto[] Permission { get; set; }
        public UserRoleDto[] Roles { get; set; }


    }
}
