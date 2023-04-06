using BankingManagement.Domain.Enums;
using BankingManagement.Domain.Transactions;

namespace BankingManagement.Domain.User
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PrivateNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PasswordHash { get; set; }
        public UserRoles Role { get; set; }

        public List<Account.Account> Accounts { get; set; }
        public List<Transaction> TransactionsSent { get; set; }
        public List<Transaction> TransactionsRecieved { get; set; }
    }
}