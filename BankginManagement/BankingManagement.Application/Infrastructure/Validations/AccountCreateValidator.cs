using BankingManagement.Application.Accounts.Requests;
using BankingManagement.Application.Infrastructure.Resources;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingManagement.Application.Infrastructure.Validations
{
    internal class AccountCreateValidator : AbstractValidator<AccountCreateRequestModel>
    {
        public AccountCreateValidator()
        {
            RuleFor(x => x.Amount)
                .NotEmpty()
                .GreaterThanOrEqualTo(0).WithMessage(ExceptionTexts.DepositAmount);

        }
    }
}
