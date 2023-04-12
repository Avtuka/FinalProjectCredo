using BankingManagement.Application.Infrastructure.Helpers;
using BankingManagement.Application.Infrastructure.Resources;
using BankingManagement.Application.Operator.Exceptions;
using BankingManagement.Application.Operator.Requests;
using BankingManagement.Application.Repositories;
using BankingManagement.Domain.Enums;
using Mapster;

namespace BankingManagement.Application.Operator
{
    internal class OperatorService : IOperatorService
    {
        #region Private Members and CTOR

        private readonly IRepository<Domain.Operator.Operator> _repo;

        public OperatorService(IRepository<Domain.Operator.Operator> repo)
        {
            _repo = repo;
        }

        #endregion Private Members and CTOR

        public async Task<Domain.Operator.Operator> AuthenticateAsync(OperatorLoginModel @operator, CancellationToken cancellationToken)
        {
            var oper = await _repo.GetByPredicateAsync(x => x.Email == @operator.Email && x.PasswordHash == MyPasswordHelper.GenerateSHA512Hash(@operator.Password), cancellationToken);

            if (oper == null)
                throw new InvalidCredentialsException(ExceptionTexts.InvalidCredentials);

            return oper;
        }

        public async Task ChangePassword(OperatorChangePasswordModel model, int operatorId, CancellationToken cancellationToken)
        {
            var oper = await _repo.GetByPredicateAsync(x => x.Id == operatorId && x.PasswordHash == MyPasswordHelper.GenerateSHA512Hash(model.CurrentPassword), cancellationToken);

            if (oper == null)
                throw new InvalidCredentialsException(ExceptionTexts.InvalidCredentials);

            oper.PasswordHash = MyPasswordHelper.GenerateSHA512Hash(model.NewPassword);

            _repo.Update(oper);
            await _repo.SaveChangesAsync(cancellationToken);
        }

        public async Task RegisterAsync(OperatorRegisterRequestModel model, CancellationToken cancellationToken)
        {
            if (await _repo.GetByPredicateAsync(x => x.Email == model.Email, cancellationToken) != null)
                throw new DuplicateEmailException(ExceptionTexts.DuplicateEmail);

            if (Enum.GetName(typeof(OperatorRoles), model.Role) == null)
                throw new Exception("Incorrect role");

            var operatorToRegister = model.Adapt<Domain.Operator.Operator>();

            var password = MyPasswordHelper.GenerateRandomPassword();

            operatorToRegister.PasswordHash = MyPasswordHelper.GenerateSHA512Hash(password);

            await _repo.AddAsync(operatorToRegister, cancellationToken);
            await _repo.SaveChangesAsync(cancellationToken);

            string message = EmailHelper.GeneratePasswordEmailForOperaotor(operatorToRegister.FirstName, password);
            EmailHelper.SendEmail(operatorToRegister.Email, "Password", message);
        }
    }
}