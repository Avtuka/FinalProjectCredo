using BankingManagement.Application.Accounts.Requests;

namespace BankingManagement.Application.Users.Requests
{
    public class UserRegisterRequestModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PrivateNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }

        public List<AccountCreateRequestModel> Accounts { get; set; }
    }
}