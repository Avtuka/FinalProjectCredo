namespace BankingManagement.Application.ATM.Exceptions
{
    public class InvalidPINException : Exception
    {
        public readonly string Code = "InvalidPin";

        public InvalidPINException(string text) : base(text)
        {
        }
    }
}