namespace BankingManagement.Application.Users.Exceptions
{
    public class EmailNotConfirmedException : Exception
    {
        public readonly string Code = "EmailNotConfrimed";

        public EmailNotConfirmedException(string text) : base(text)
        {
        }
    }
}