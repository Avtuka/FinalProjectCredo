namespace BankingManagement.Application.Operator.Exceptions
{
    public class InvalidCredentialsException : Exception
    {
        public readonly string Code = "InvalidCredentials";

        public InvalidCredentialsException(string text) : base(text)
        {
        }
    }
}