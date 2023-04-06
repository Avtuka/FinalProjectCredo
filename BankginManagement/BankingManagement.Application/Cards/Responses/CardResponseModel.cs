using BankingManagement.Application.Accounts.Responses;

namespace BankingManagement.Application.Cards.Responses
{
    public class CardResponseModel
    {
        public int Id { get; set; }
        public string CardNumber { get; set; }
        public string FullName { get; set; }
        public DateTime ExpirationDate { get; set; }
        public short CVC { get; set; }
        public short PIN { get; set; }

        public List<AccountResponseModel> Accounts { get; set; }
    }
}