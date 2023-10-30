using HealthInsuranceERP.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace HealthInsuranceERP.Core.Exceptions
{
    public class NotFoundException : BaseException
    {
        public NotFoundException(string message) : base(message)
        {
            httpStatusCode = HttpStatusCode.BadRequest;
            Code = ResponseCodes.NotFound;
        }
    }
}
