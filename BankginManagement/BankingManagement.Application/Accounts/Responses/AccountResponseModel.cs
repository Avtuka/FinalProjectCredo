using BankingManagement.Domain.Enums;

namespace BankingManagement.Application.Accounts.Responses
{
    public class AccountResponseModel
    {
        public int Id { get; set; }
        public int OwnerId { get; set; }
        public int CardId { get; set; }
        public string IBAN { get; set; }
        public double Amount { get; set; }
        public Currencies Currency { get; set; }
    }
}