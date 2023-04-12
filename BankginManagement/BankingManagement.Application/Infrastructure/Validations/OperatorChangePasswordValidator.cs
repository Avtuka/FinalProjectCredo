using BankingManagement.Application.Operator.Requests;
using FluentValidation;

namespace BankingManagement.Application.Infrastructure.Validations
{
    public class OperatorChangePasswordValidator : AbstractValidator<OperatorChangePasswordModel>
    {
        public OperatorChangePasswordValidator()
        {
            RuleFor(x => x.NewPassword)
              .Matches(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*[!@#$%^&*])(?=.*\d)[A-Za-z\d!@#$%^+=';:?/\\&\-* \[\]{}]{8,}$");

            RuleFor(x => x.ConfirmPassword)
                .Equal(x => x.NewPassword);
        }
    }
}