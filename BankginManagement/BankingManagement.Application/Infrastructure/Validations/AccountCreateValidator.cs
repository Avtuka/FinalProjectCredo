using BankingManagement.Application.Accounts.Requests;
using BankingManagement.Application.Infrastructure.Resources;
using FluentValidation;

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