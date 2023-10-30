using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using HealthInsuranceERP.Core.Exceptions;
namespace HealthInsuranceERP.Infrastructure.Utilities
{
    public class Log<T>
    {
        private static readonly ILogger _logger = LoggerFactory.Create(builder =>
        {
            builder
                .AddFilter("Microsoft", LogLevel.Warning)
                .AddFilter("System", LogLevel.Warning);
        }).CreateLogger<T>();

        public static void LogDebug(Exception ex)
        {
            _ = Task.Run(() =>
            {
                _logger.LogDebug(ex, ex.Message, ErrorDetails(ex));
            });
        }

        public static void LogError(Exception ex)
        {
            _ = Task.Run(() =>
            {
                string[] errorDetails = ErrorDetails(ex);
                _logger.LogError(ex, String.Join("|", errorDetails), errorDetails);
            });
        }

        public static void LogInformation(string msg)
        {
            _ = Task.Run(() =>
            {
                _logger.LogInformation(msg);
            });
        }

        public static void LogWarning(Exception ex)
        {
            _ = Task.Run(() =>
            {
                string[] errorDetails = ErrorDetails(ex);
                _logger.LogWarning(ex, String.Join("|", errorDetails), errorDetails);
            });
        }

        private static string[] ErrorDetails(Exception ex)
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
