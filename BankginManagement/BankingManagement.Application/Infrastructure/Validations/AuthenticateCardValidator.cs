using BankingManagement.Application.ATM.Requests;
using BankingManagement.Application.Infrastructure.Resources;
using FluentValidation;

namespace BankingManagement.Application.Infrastructure.Validations
{
    public class AuthenticateCardValidator : AbstractValidator<AuthenticateCardRequestModel>
    {
        public AuthenticateCardValidator()
        {
            RuleFor(x => x.CardNumber)
                .NotEmpty().WithMessage(ExceptionTexts.CreditCardNumberRequired)
                .CreditCard().WithMessage(ExceptionTexts.InvalidCreditCardNumber);

            RuleFor(x => x.PIN)
                .NotEmpty().WithMessage(ExceptionTexts.PINRequired);
        }
    }
}