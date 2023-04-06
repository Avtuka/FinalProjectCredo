namespace BankingManagement.Application.Cards.Requests
{
    internal class CardCreateRequestModel
    {
        public string CardNumber { get; set; }
        public DateTime ExpirationDate { get; set; }
        public short CVC { get; set; }
        public short PIN { get; set; }
    }
}