using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInsuranceERP.Core.Utilities
{
    public static class ResponseCodes
    {
        public const string Successful = "00";
        public const string Failed = "99";
        public const string AlreadyExist = "01";
        public const string NotFound = "02";
        public const string ModelValidation = "03";
        public const string Forbidden = "04";
        public static string Unauthorized = "05";
        public static string ProcessorError = "06";
        public static string TimeOut = "91";

        public static string TokenExpired = "81";
        public static string TokenValidationFailed = "82";
        public static string BadReqeust = "83";
    }
}
