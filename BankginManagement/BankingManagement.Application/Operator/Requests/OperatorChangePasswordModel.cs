namespace BankingManagement.Application.Operator.Requests
{
    public class OperatorChangePasswordModel
    {
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
}