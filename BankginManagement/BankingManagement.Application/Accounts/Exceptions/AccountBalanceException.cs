namespace BankingManagement.Application.Accounts.Exceptions
{
    public class AccountBalanceException : Exception
    {
        public readonly string Code = "InsufficientBalance";

        public AccountBalanceException(string text) : base(text)
        {
        }
    }
}