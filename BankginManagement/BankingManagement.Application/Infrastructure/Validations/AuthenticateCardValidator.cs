using BankingManagement.Application.ATM.Requests;
using BankingManagement.Application.Infrastructure.Resources;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
