
using HealthInsuranceERP.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInsuranceERP.Core.Models
{
    public class ResponseModel<T>
    {
        public bool RequestSuccessful { get; set; }
        public T ResponseData { get; set; }
        public string Message { get; set; }
        public string ResponseCode { get; set; }

        public ResponseModel()
        {
        }
        public ResponseModel(bool requestSuccessful, T Data, string message,
            string responseCode)
        {
            this.RequestSuccessful = requestSuccessful;
            this.ResponseData = Data;
            this.Message = message;
            this.ResponseCode = responseCode;
        }

        public static ResponseModel<T> IsSuccessful(T data, string message)
        {
            return new ResponseModel<T>(true, data,
                message, ResponseCodes.Successful);
        }

        public static ResponseModel<T> Failed(T data, string message)
        {
            return new ResponseModel<T>(false, data,
                message, ResponseCodes.Failed);
        }

        public static ResponseModel<T> SetResponse(T data, string message, string responseCode, bool isSuccessful)
        {
            return new ResponseModel<T>(isSuccessful, data,
                message, responseCode);
        }
    }
}