namespace BankingManagement.Application.Transaction.Exceptions
{
    public class EmptyTransactionException : Exception
    {
        public readonly string Code = "EmptyTransaction";

        public EmptyTransactionException(string text) : base(text)
        {
        }
    }
}