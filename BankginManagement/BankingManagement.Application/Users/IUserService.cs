using BankingManagement.Application.Users.Requests;
using BankingManagement.Domain.User;

namespace BankingManagement.Application.Users
{
    public interface IUserService
    {
        Task<User> Authenticate(UserLoginRequestModel model, CancellationToken cancellationToken);
        Task ConfirmEmailAsync(string code, string secret, CancellationToken cancellationToken);
        Task RegisterAsync(UserRegisterRequestModel model, string secret, CancellationToken cancellationToken);
    }
}