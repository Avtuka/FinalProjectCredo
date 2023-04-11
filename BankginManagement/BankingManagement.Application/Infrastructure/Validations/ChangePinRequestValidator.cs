using BankingManagement.Application.ATM.Requests;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingManagement.Application.Infrastructure.Validations
{
    public class ChangePinRequestValidator : AbstractValidator<ChangePinRequestModel>
    {
        public ChangePinRequestValidator()
        {
            RuleFor(x => x.CurrentPassword)
                .NotEmpty().WithMessage("PIN is required");

            RuleFor(x => x.NewPassword)
                .NotEmpty().WithMessage("PIN is required");

            RuleFor(x => x.ConfirmPassword)
                .Equal(x => x.NewPassword).WithMessage("PINs do not match");
        }
    }
}
