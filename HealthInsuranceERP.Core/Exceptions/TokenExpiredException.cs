using HealthInsuranceERP.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace HealthInsuranceERP.Core.Exceptions
{
    public class TokenExpiredException : BaseException
    {
        public TokenExpiredException(string message) : base(message)
        {
            httpStatusCode = HttpStatusCode.BadRequest;
            Code = ResponseCodes.TokenExpired;
        }
    }
}
