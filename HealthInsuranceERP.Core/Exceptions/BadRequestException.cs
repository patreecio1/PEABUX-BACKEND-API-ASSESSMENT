using System;
using System.Collections.Generic;
using HealthInsuranceERP.Core.Utilities;
using System.Net;

namespace HealthInsuranceERP.Core.Exceptions
{
    public class BadRequestException : BaseException
    {
        public BadRequestException(string message) : base(message)
        {
            httpStatusCode = HttpStatusCode.BadRequest;
            Code = ResponseCodes.BadReqeust;
        }
    }
}
