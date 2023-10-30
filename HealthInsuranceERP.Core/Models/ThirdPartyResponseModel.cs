using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;


namespace HealthInsuranceERP.Core.Models
{
    public class ThirdPartyResponseModel<T>
    {
        public bool IsSuccessful { get; set; }
        public T Content { get; set; }
        public string Error { get; set; }
        public string ResponseCode { get; set; }
        public string RequestPayload { get; set; }
        public string ResponsePayload { get; set; }
        public string Message { get; set; }
        public HttpStatusCode Status { get; set; }
    }
}
