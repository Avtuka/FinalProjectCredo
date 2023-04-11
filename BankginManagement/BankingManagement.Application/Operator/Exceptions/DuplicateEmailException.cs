namespace BankingManagement.Application.Operator.Exceptions
{
    public class DuplicateEmailException : Exception
    {
        public readonly string Code = "DuplicateEmail";

        public DuplicateEmailException(string text) : base(text)
        {
        }
    }
}