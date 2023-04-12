namespace BankingManagement.Application.Users.Requests
{
    public class UserPasswordChangeModel
    {
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
}