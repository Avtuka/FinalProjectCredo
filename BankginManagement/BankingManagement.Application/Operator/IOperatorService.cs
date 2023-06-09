﻿using BankingManagement.Application.Operator.Requests;

namespace BankingManagement.Application.Operator
{
    public interface IOperatorService
    {
        Task<Domain.Operator.Operator> AuthenticateAsync(OperatorLoginModel @operator, CancellationToken cancellationToken);

        Task ChangePassword(OperatorChangePasswordModel model, int operatorId, CancellationToken cancellationToken);

        Task RegisterAsync(OperatorRegisterRequestModel model, CancellationToken cancellationToken);
    }
}