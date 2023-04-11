namespace BankingManagement.Application.ATM.Exceptions
{
    public class InvalidWithdrawAmountException : Exception
    {
        public readonly string Code = "InvalidAmount";

        public InvalidWithdrawAmountException(string text) : base(text)
        {
        }
    }
}