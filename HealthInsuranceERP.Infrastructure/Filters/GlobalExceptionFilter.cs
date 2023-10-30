
using Microsoft.IdentityModel.Tokens;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;

using Microsoft.AspNetCore.Mvc.Filters;
using HealthInsuranceERP.Core.Models;
using HealthInsuranceERP.Core.Utilities;
using HealthInsuranceERP.Core.Exceptions;

namespace HealthInsuranceERP.Infrastructure.Filters
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
        }

        public static (ResponseModel<T> responseModel, HttpStatusCode statusCode) GetStatusCode<T>(Exception exception)
        {
            switch (exception)
            {
                case BaseException bex:
                    return (new ResponseModel<T>
                    {
                        ResponseCode = bex.Code,
                        Message = bex.Message,
                        RequestSuccessful = false
                    }, bex.httpStatusCode);
                case SecurityTokenExpiredException bex:
                    return (new ResponseModel<T>
                    {
                        ResponseCode = ResponseCodes.TokenExpired,
                        Message = "Session expired",
                        RequestSuccessful = false,
                    }, HttpStatusCode.Unauthorized);
                case SecurityTokenValidationException bex:
                    return (new ResponseModel<T>
                    {
                        ResponseCode = ResponseCodes.TokenValidationFailed,
                        Message = "Invalid authentication parameters",
                        RequestSuccessful = false,
                    }, HttpStatusCode.Unauthorized);
                case ValidationException bex:
                    return (new ResponseModel<T>
                    {
                        ResponseCode = ResponseCodes.ModelValidation,
                        Message = bex.Message,
                        RequestSuccessful = false
                    }, HttpStatusCode.BadRequest);
                default:
                    return (new ResponseModel<T>
                    {
                        ResponseCode = ResponseCodes.Failed,
                        Message = exception.Message,
                        RequestSuccessful = false
                    }, HttpStatusCode.InternalServerError);
            }
        }
    }

}
