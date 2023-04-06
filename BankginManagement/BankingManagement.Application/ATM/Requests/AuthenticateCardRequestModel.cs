namespace BankingManagement.Application.ATM.Requests
{
    public class AuthenticateCardRequestModel
    {
        public string CardNumber { get; set; }
        public short PIN { get; set; }
    }
}
