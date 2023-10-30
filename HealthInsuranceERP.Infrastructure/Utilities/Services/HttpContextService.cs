using HealthInsuranceERP.Core.Interfaces.Services;
using HealthInsuranceERP.Infrastructure.Utilities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
namespace HealthInsuranceERP.Infrastructure.Services
{
    public class HttpContextService : IHttpContextService
    {

        // private readonly IMemoryCache memoryCache;
        private readonly IHttpService httpService;
        private readonly IHttpContextAccessor _httpAccessor;

        public HttpContextService(IHttpService httpService, IHttpContextAccessor httpContextAccessor)
        {
            _httpAccessor = httpContextAccessor;


            this.httpService = httpService;
        }

        public string CurrentUserId()
        {
            try
            {
                var claim = _httpAccessor.HttpContext.User.Claims
                       .Where(x => x.Type == ClaimTypes.NameIdentifier)
                       .FirstOrDefault();

                return claim.Value;
            }
            catch
            {
                return null;
            }
        }
        public string CurrentClientId()
        {
            try
            {
                var claim = _httpAccessor.HttpContext.User.Claims
                       .Where(x => x.Type == "client_id")
                       .FirstOrDefault();
                if (claim == null)
                    return null;

                return claim.Value;
            }
            catch
            {
                return null;
            }
        }
        public string CurrentUsername()
        {
            try
            {
                var claim = _httpAccessor.HttpContext.User.Claims
                       .Where(x => x.Type == ClaimTypes.Email)
                       .FirstOrDefault();


                return claim.Value;
            }
            catch (Exception ex)
            {
                Log<IHttpContextService>.LogError(ex);

                return null;
            }
        }

        public string GetCalerIP()
        {
            var e = _httpAccessor.HttpContext.Request.Headers;
            if (e.ContainsKey("X-Forwarded-For"))
            {
                string ip = e["X-Forwarded-For"];
                ip = ip.Replace("[", string.Empty);
                ip = ip.Replace("\\", string.Empty);
                ip = ip.Replace("]", string.Empty);
                ip = ip.Replace("\"", string.Empty);
                return ip;
            }

            return "127.0.0.1";
        }
    }
}
