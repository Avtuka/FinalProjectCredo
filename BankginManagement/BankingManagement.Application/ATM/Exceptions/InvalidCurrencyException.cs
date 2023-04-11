namespace BankingManagement.Application.ATM.Exceptions
{
    public class InvalidCurrencyException : Exception
    {
        public readonly string Code = "InvalidCurrency";

        public InvalidCurrencyException(string text) : base(text)
        {
        }
    }
}