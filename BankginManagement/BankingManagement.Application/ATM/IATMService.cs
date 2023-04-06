using BankingManagement.Application.ATM.Requests;
using BankingManagement.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingManagement.Application.ATM
{
    public interface IATMService
    {
        Task<List<KeyValuePair<Currencies, double>>> SeeBalanceAsync(AuthenticateCardRequestModel authenticateModel, CancellationToken cancellationToken);
        Task ChangePin(AuthenticateCardRequestModel authenticateModel, ChangePinRequestModel pinModel, CancellationToken cancellationToken);
        Task WithdrawAsync(AuthenticateCardRequestModel authenticateModel, Currencies currency, double amount, CancellationToken cancellationToken);
    }
}
