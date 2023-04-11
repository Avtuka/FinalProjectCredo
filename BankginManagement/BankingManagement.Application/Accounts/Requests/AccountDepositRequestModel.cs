using BankingManagement.Domain.Enums;

namespace BankingManagement.Application.Accounts.Requests
{
    public class AccountDepositRequestModel
    {
        public string IBAN { get; set; }
        public double Amount { get; set; }
        public Currencies Currency { get; set; }
    }
}