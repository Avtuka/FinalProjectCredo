using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BankingManagement.InsideSystem.API
{
    public class APIError : ProblemDetails
    {
        private HttpContext _httpContext;
        private Exception _exception;

        public const string UnhandlerErrorCode = "UnhandledError";
        public LogLevel LogLevel { get; set; }
        public string Code { get; set; }

        public string TraceId
        {
            get
            {
                if (Extensions.TryGetValue("TraceId", out var traceId))
                {
                    return (string)traceId!;
                }

                return null;
            }

            set => Extensions["TraceId"] = value;
        }

        public APIError(HttpContext httpContext, Exception exception)
        {
            _httpContext = httpContext;
            _exception = exception;

            TraceId = httpContext.TraceIdentifier;
            Instance = httpContext.Request.Path;

            HandleException((dynamic)exception);
        }

        private void HandleException(Exception exception)
        {
            Code = UnhandlerErrorCode;
            Status = (int)HttpStatusCode.InternalServerError;
            Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.6.1";
            Title = exception.Message;
            LogLevel = LogLevel.Error;
        }
    }
}