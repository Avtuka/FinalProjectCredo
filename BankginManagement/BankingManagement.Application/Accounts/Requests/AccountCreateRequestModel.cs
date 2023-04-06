using BankingManagement.Domain.Enums;

namespace BankingManagement.Application.Accounts.Requests
{
    public class AccountCreateRequestModel
    {
        public double Amount { get; set; }
        public Currencies Currency { get; set; }
    }
}