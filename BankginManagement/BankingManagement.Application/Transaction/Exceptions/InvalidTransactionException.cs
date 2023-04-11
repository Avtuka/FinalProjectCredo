namespace BankingManagement.Application.Transaction.Exceptions
{
    public class InvalidTransactionException : Exception
    {
        public readonly string Code = "InvalidTransaction";

        public InvalidTransactionException(string text) : base(text)
        {
        }
    }
}