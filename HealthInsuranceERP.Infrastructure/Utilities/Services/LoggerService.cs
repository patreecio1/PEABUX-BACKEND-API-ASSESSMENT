using HealthInsuranceERP.Core.Exceptions;
using HealthInsuranceERP.Core.Interfaces.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace HealthInsuranceERP.Infrastructure.Services
{
    public class LoggerService<T> : ILoggerService<T>
    {
        private readonly ILogger _logger;
        public LoggerService(ILogger<T> logger)
        {
            _logger = logger;
        }

        public void LogDebug(Exception ex)
        {
            _ = Task.Run(() =>
            {
                _logger.LogDebug(ex, ex.Message, ErrorDetails(ex));
            });
        }

        public void LogError(Exception ex)
        {
            _ = Task.Run(() =>
            {
                string[] errorDetails = ErrorDetails(ex);
                _logger.LogError(ex, String.Join("|", errorDetails), errorDetails);
            });
        }

        public void LogInformation(string msg)
        {
            _ = Task.Run(() =>
            {
                _logger.LogInformation(msg);
            });
        }

        public void LogWarning(Exception ex)
        {
            _ = Task.Run(() =>
            {
                string[] errorDetails = ErrorDetails(ex);
                _logger.LogWarning(ex, String.Join("|", errorDetails), errorDetails);
            });
        }

        private string[] ErrorDetails(Exception ex)
        {
            if (ex is BaseException)
            {
                var x = (BaseException)ex;
                return new string[] {
                $"Message => {x.Message}",
                $"Stacktrace => {x.StackTrace}",
                $"InnerException => {x.InnerException}",
                $"HttpStatus => {x.httpStatusCode}"
                };
            }
            else
            {
                return new string[] {
                $"Message => {ex.Message}",
                $"Stacktrace => {ex.StackTrace}",
                $"InnerException => {ex.InnerException}"
                };
            }
        }
    }

}
