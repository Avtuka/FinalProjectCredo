using BankingManagement.Application.Users.Requests;
using FluentValidation;

namespace BankingManagement.Application.Infrastructure.Validations
{
    public class UserChangePasswordValidator : AbstractValidator<UserPasswordChangeModel>
    {
        public UserChangePasswordValidator()
        {
            RuleFor(x => x.NewPassword)
                .Matches(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*[!@#$%^&*])(?=.*\d)[A-Za-z\d!@#$%^+=';:?/\\&\-* \[\]{}]{8,}$");

            RuleFor(x => x.ConfirmPassword)
                .Equal(x => x.NewPassword);
        }
    }
}