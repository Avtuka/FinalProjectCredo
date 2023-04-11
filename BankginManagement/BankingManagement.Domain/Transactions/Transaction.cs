using BankingManagement.Domain.Enums;

namespace BankingManagement.Domain.Transactions
{
    public class Transaction
    {
        public int Id { get; set; }
        public string? FromIBAN { get; set; }
        public int SenderId { get; set; }
        public string ToIBAN { get; set; }
        public int RecieverId { get; set; }
        public double Amount { get; set; }
        public Currencies Currency { get; set; }
        public DateTime Date { get; set; }
        public double Comission { get; set; }
        public TransactionTypes TransactionType { get; set; }

        public User.User Sender { get; set; }
        public User.User Reciever { get; set; }
    }
}