using BankingManagement.Domain.BaseEntity;
using BankingManagement.Domain.Enums;

namespace BankingManagement.Domain.Operator
{
    public class Operator : IBaseEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PrivateNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public OperatorRoles Role { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
    }
}