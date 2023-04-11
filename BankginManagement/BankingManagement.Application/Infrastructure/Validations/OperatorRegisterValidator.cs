using BankingManagement.Application.Operator.Requests;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingManagement.Application.Infrastructure.Validations
{
    public class OperatorRegisterValidator : AbstractValidator<OperatorRegisterRequestModel>
    {
        public OperatorRegisterValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("Firstname is required")
                .MaximumLength(40).WithMessage("Firstname length must not be more than 40 characters");

            RuleFor(x => x.LastName)
              .NotEmpty().WithMessage("Lastname is required")
              .MaximumLength(50).WithMessage("Lastname length must not be more than 50 characters");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Email must be in correct format");

            RuleFor(x => x.PrivateNumber)
                .NotEmpty().WithMessage("Private number is required")
                .Length(11).WithMessage("Private number must contain exavtly 11 numbers")
                .Matches("^([0-9]{11})$").WithMessage("Private number can only contain digits");

            RuleFor(x => x.DateOfBirth)
                .NotEmpty().WithMessage("Date of birth is required")
                .GreaterThanOrEqualTo(DateTime.UtcNow.AddYears(-18)).WithMessage("Operator must be more than 18 years");
        }
    }
}
