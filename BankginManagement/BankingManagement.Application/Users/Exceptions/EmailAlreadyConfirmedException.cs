namespace BankingManagement.Application.Users.Exceptions
{
    public class EmailAlreadyConfirmedException : Exception
    {
        public readonly string Code = "EmailAlreadyConfirmed";

        public EmailAlreadyConfirmedException(string text) : base(text)
        {
        }
    }
}