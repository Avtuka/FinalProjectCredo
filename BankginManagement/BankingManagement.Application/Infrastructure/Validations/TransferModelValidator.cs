using BankingManagement.Application.Accounts.Requests;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingManagement.Application.Infrastructure.Validations
{
    public class TransferModelValidator : AbstractValidator<TransferModelRequest>
    {
        public TransferModelValidator()
        {
            RuleFor(x => x.FromIBAN)
                .NotEmpty().WithMessage("IBAN must not be empty");

            RuleFor(x => x.ToIBAN)
                .NotEmpty().WithMessage("IBAN must not be empty");

            RuleFor(x => x.Amount)
                .NotEmpty()
                .GreaterThan(0).WithMessage("Transfer money must be greater than 0");
        }
    }
}
