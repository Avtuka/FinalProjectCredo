namespace BankingManagement.Application.ATM.Requests
{
    public class ChangePinRequestModel
    {
        public short CurrentPassword { get; set; }
        public short NewPassword { get; set; }
        public short ConfirmPassword { get; set; }
    }
}