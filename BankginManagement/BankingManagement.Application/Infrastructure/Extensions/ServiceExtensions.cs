using BankingManagement.Application.Accounts;
using BankingManagement.Application.ATM;
using BankingManagement.Application.Cards;
using BankingManagement.Application.Operator;
using BankingManagement.Application.Rates;
using BankingManagement.Application.Reports;
using BankingManagement.Application.Transaction;
using BankingManagement.Application.Users;
using Microsoft.Extensions.DependencyInjection;

namespace BankingManagement.Application.Infrastructure.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IOperatorService, OperatorService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<ICardService, CardService>();
            services.AddScoped<ITransactionService, TransactionService>();
            services.AddScoped<IRateService, RateService>();
            services.AddScoped<IATMService, ATMService>();
            services.AddScoped<IReportService, ReportService>();

            return services;
        }
    }
}