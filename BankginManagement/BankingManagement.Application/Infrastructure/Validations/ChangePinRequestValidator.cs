using BankingManagement.Application.ATM.Requests;
using BankingManagement.Application.Infrastructure.Resources;
using FluentValidation;

namespace BankingManagement.Application.Infrastructure.Validations
{
    public class ChangePinRequestValidator : AbstractValidator<ChangePinRequestModel>
    {
        public ChangePinRequestValidator()
        {
            RuleFor(x => x.CurrentPassword)
                .NotEmpty().WithMessage(ExceptionTexts.PINRequired);

            RuleFor(x => x.NewPassword)
                .NotEmpty().WithMessage(ExceptionTexts.PINRequired);

            RuleFor(x => x.ConfirmPassword)
                .Equal(x => x.NewPassword).WithMessage(ExceptionTexts.PINsDoNotMatch);
        }
    }
}