using BankingManagement.Application.Accounts.Requests;
using BankingManagement.Application.Accounts.Responses;

namespace BankingManagement.Application.Accounts
{
    public interface IAccountService
    {
        Task<bool> IbanExistsAsync(string iban, CancellationToken cancellationToken);

        Task<List<AccountResponseModel>> GetAccounts(int userId, CancellationToken cancellationToken);

        Task TransferToOwnAccountAsync(TransferModelRequest model, int userId, CancellationToken cancellationToken);

        Task TransferToOtherAccountAsync(TransferModelRequest model, int userId, CancellationToken cancellationToken);

        Task DepositAsync(AccountDepositRequestModel model, int userId, CancellationToken cancellationToken);
    }
}