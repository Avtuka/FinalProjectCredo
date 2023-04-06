using BankingManagement.Domain.Enums;

namespace BankingManagement.Application.Operator.Requests
{
    public class OperatorRegisterRequestModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PrivateNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public OperatorRoles Role { get; set; }
    }
}