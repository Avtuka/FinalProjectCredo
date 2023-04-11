using BankingManagement.Application.ATM.Requests;
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
                .NotEmpty().WithMessage("Credit card number is required")
                .CreditCard().WithMessage("Invalid credit card number");

            RuleFor(x => x.PIN)
                .NotEmpty().WithMessage("Pin is required");
        }
    }
}
