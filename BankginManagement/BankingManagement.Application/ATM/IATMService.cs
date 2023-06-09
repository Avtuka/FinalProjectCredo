﻿using BankingManagement.Application.ATM.Requests;
using BankingManagement.Domain.Enums;

namespace BankingManagement.Application.ATM
{
    public interface IATMService
    {
        Task<List<KeyValuePair<string, double>>> SeeBalanceAsync(AuthenticateCardRequestModel authenticateModel, CancellationToken cancellationToken);

        Task ChangePin(AuthenticateCardRequestModel authenticateModel, ChangePinRequestModel pinModel, CancellationToken cancellationToken);

        Task WithdrawAsync(AuthenticateCardRequestModel authenticateModel, Currencies currency, double amount, CancellationToken cancellationToken);
    }
}