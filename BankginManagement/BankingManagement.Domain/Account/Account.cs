using BankingManagement.Domain.BaseEntity;
using BankingManagement.Domain.Enums;

namespace BankingManagement.Domain.Account
{
    public class Account : IBaseEntity
    {
        public int Id { get; set; }
        public int OwnerId { get; set; }
        public int CardId { get; set; }
        public string IBAN { get; set; }
        public double Amount { get; set; }
        public Currencies Currency { get; set; }

        public User.User Owner { get; set; }
        public Card.Card Card { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
    }
}