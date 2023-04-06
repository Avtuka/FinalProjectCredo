namespace BankingManagement.Domain.Card
{
    public class Card
    {
        public int Id { get; set; }
        public string CardNumber { get; set; }
        public string FullName { get; set; }
        public DateTime ExpirationDate { get; set; }
        public short CVC { get; set; }
        public short PIN { get; set; }

        public List<Account.Account> Accounts { get; set; }
    }
}