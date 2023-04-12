using BankingManagement.Application.Accounts.Exceptions;
using BankingManagement.Application.ATM.Exceptions;
using BankingManagement.Application.Cards.Exceptions;
using BankingManagement.Application.Operator.Exceptions;
using BankingManagement.Application.Transaction.Exceptions;
using BankingManagement.Application.Users.Exceptions;
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

        private void HandleException(AccountBalanceException exception)
        {
            Code = exception.Code;
            Status = (int)HttpStatusCode.BadRequest;
            Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1";
            Title = exception.Message;
            LogLevel = LogLevel.Error;
        }

        private void HandleException(AccountNotFoundException exception)
        {
            Code = exception.Code;
            Status = (int)HttpStatusCode.BadRequest;
            Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1";
            Title = exception.Message;
            LogLevel = LogLevel.Error;
        }

        private void HandleException(InvalidTransactionException exception)
        {
            Code = exception.Code;
            Status = (int)HttpStatusCode.BadRequest;
            Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1";
            Title = exception.Message;
            LogLevel = LogLevel.Error;
        }

        private void HandleException(InvalidCurrencyException exception)
        {
            Code = exception.Code;
            Status = (int)HttpStatusCode.BadRequest;
            Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1";
            Title = exception.Message;
            LogLevel = LogLevel.Error;
        }

        private void HandleException(InvalidPINException exception)
        {
            Code = exception.Code;
            Status = (int)HttpStatusCode.BadRequest;
            Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1";
            Title = exception.Message;
            LogLevel = LogLevel.Error;
        }

        private void HandleException(InvalidWithdrawAmountException exception)
        {
            Code = exception.Code;
            Status = (int)HttpStatusCode.BadRequest;
            Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1";
            Title = exception.Message;
            LogLevel = LogLevel.Error;
        }

        private void HandleException(WithdrawLimitException exception)
        {
            Code = exception.Code;
            Status = (int)HttpStatusCode.BadRequest;
            Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1";
            Title = exception.Message;
            LogLevel = LogLevel.Error;
        }

        private void HandleException(NoCardsException exception)
        {
            Code = exception.Code;
            Status = (int)HttpStatusCode.BadRequest;
            Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1";
            Title = exception.Message;
            LogLevel = LogLevel.Error;
        }

        private void HandleException(InvalidCredentialsException exception)
        {
            Code = exception.Code;
            Status = (int)HttpStatusCode.BadRequest;
            Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1";
            Title = exception.Message;
            LogLevel = LogLevel.Error;
        }

        private void HandleException(DuplicateEmailException exception)
        {
            Code = exception.Code;
            Status = (int)HttpStatusCode.BadRequest;
            Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1";
            Title = exception.Message;
            LogLevel = LogLevel.Error;
        }

        private void HandleException(EmptyTransactionException exception)
        {
            Code = exception.Code;
            Status = (int)HttpStatusCode.BadRequest;
            Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1";
            Title = exception.Message;
            LogLevel = LogLevel.Error;
        }

        private void HandleException(EmailAlreadyConfirmedException exception)
        {
            Code = exception.Code;
            Status = (int)HttpStatusCode.BadRequest;
            Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1";
            Title = exception.Message;
            LogLevel = LogLevel.Error;
        }

        private void HandleException(EmailNotConfirmedException exception)
        {
            Code = exception.Code;
            Status = (int)HttpStatusCode.BadRequest;
            Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1";
            Title = exception.Message;
            LogLevel = LogLevel.Error;
        }

        private void HandleException(MoreThanOneAccountException exception)
        {
            Code = exception.Code;
            Status = (int)HttpStatusCode.BadRequest;
            Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1";
            Title = exception.Message;
            LogLevel = LogLevel.Error;
        }

        private void HandleException(Exception exception)
        {
            Code = UnhandlerErrorCode;
            Status = (int)HttpStatusCode.InternalServerError;
            Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.6.1";
            Title = exception.Message;
            LogLevel = LogLevel.Critical;
        }
    }
}