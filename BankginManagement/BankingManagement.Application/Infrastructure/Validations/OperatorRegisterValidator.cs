using BankingManagement.Application.Infrastructure.Resources;
using BankingManagement.Application.Operator.Requests;
using FluentValidation;

namespace BankingManagement.Application.Infrastructure.Validations
{
    public class OperatorRegisterValidator : AbstractValidator<OperatorRegisterRequestModel>
    {
        public OperatorRegisterValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage(ExceptionTexts.FirstNameRequired)
                .MaximumLength(40).WithMessage(ExceptionTexts.FirstNameLength);

            RuleFor(x => x.LastName)
              .NotEmpty().WithMessage(ExceptionTexts.LastNameRequired)
              .MaximumLength(50).WithMessage(ExceptionTexts.LastNameLength);

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage(ExceptionTexts.EmailRequired)
                .EmailAddress().WithMessage(ExceptionTexts.EmailFormat);

            RuleFor(x => x.PrivateNumber)
                .NotEmpty().WithMessage(ExceptionTexts.PrivateNumberRequired)
                .Length(11).WithMessage(ExceptionTexts.PrivateNumberLength)
                .Matches("^([0-9]{11})$").WithMessage(ExceptionTexts.PrivateNumberFormat);

            RuleFor(x => x.DateOfBirth)
                .NotEmpty().WithMessage(ExceptionTexts.DateOfBirthRequired)
                .GreaterThanOrEqualTo(DateTime.UtcNow.AddYears(-18)).WithMessage(ExceptionTexts.DateOfBirthAge);
        }
    }
}