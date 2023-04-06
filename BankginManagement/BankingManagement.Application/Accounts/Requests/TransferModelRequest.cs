namespace BankingManagement.Application.Accounts.Requests
{
    public class TransferModelRequest
    {
        public string FromIBAN { get; set; }
        public string ToIBAN { get; set; }
        public double Amount { get; set; }
    }
}