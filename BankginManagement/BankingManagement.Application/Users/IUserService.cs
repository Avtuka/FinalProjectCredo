using BankingManagement.Application.Users.Requests;
using BankingManagement.Domain.User;

namespace BankingManagement.Application.Users
{
    public interface IUserService
    {
        Task<User> Authenticate(UserLoginRequestModel model, CancellationToken cancellationToken);

        Task RegisterAsync(UserRegisterRequestModel model, CancellationToken cancellation);
    }
}