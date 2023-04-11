namespace BankingManagement.Application.ATM.Exceptions
{
    public class WithdrawLimitException : Exception
    {
        public readonly string Code = "LimitReached";

        public WithdrawLimitException(string text) : base(text)
        {
        }
    }
}