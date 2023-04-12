using BankingManagement.Application.Accounts.Exceptions;
using BankingManagement.Application.Accounts.Requests;
using BankingManagement.Application.Accounts.Responses;
using BankingManagement.Application.Infrastructure.Resources;
using BankingManagement.Application.Rates;
using BankingManagement.Application.Repositories;
using BankingManagement.Application.Transaction;
using BankingManagement.Application.Transaction.Exceptions;
using BankingManagement.Domain.Account;
using BankingManagement.Domain.Enums;
using Mapster;
using System.Data;

namespace BankingManagement.Application.Accounts
{
    internal class AccountService : IAccountService
    {
        #region Private Members and CTOR

        private readonly IRepository<Account> _repo;
        private readonly ITransactionService _transactionService;
        private readonly IRateService _rateService;

        public AccountService(IRepository<Account> repo, ITransactionService transactionService, IRateService rateService)
        {
            _repo = repo;
            _transactionService = transactionService;
            _rateService = rateService;
        }

        #endregion Private Members and CTOR

        public async Task DepositAsync(AccountDepositRequestModel model, int userId, CancellationToken cancellationToken)
        {
            _repo.BeginTransaction(IsolationLevel.RepeatableRead);

            var depositAcc = await _repo.GetByPredicateAsync(x => x.IBAN == model.IBAN, cancellationToken);
            if (depositAcc == null)
                throw new AccountNotFoundException(ExceptionTexts.AccountNotFound);

            depositAcc.Amount += model.Amount;
            var transaction = new Domain.Transactions.Transaction
            {
                SenderId = userId,
                ToIBAN = model.IBAN,
                RecieverId = userId,
                Amount = model.Amount,
                Currency = model.Currency,
                Date = DateTime.UtcNow,
                Comission = 0,
                TransactionType = TransactionTypes.Deposit
            };
            await _transactionService.AddTransactionAsync(transaction, cancellationToken);

            _repo.Update(depositAcc);
            await _repo.SaveChangesAsync(cancellationToken);

            _repo.CommitTransaction();
        }

        public async Task<List<AccountResponseModel>> GetAccounts(int userId, CancellationToken cancellationToken)
        {
            var accounts = await _repo.GetAllWithIncludeAsync(cancellationToken, x => x.OwnerId == userId, x => x.Card);

            if (accounts.Count() == 0)
                throw new AccountNotFoundException(ExceptionTexts.AccountNotFound);

            return accounts.Adapt<List<AccountResponseModel>>();
        }

        public async Task<bool> IbanExistsAsync(string iban, CancellationToken cancellationToken)
        {
            var acc = await _repo.GetByPredicateAsync(x => x.IBAN == iban, cancellationToken);

            return acc != null;
        }

        public async Task TransferToOtherAccountAsync(TransferModelRequest model, int userId, CancellationToken cancellationToken)
        {
            _repo.BeginTransaction(IsolationLevel.RepeatableRead);

            var from = await _repo.GetByPredicateAsync(x => x.IBAN == model.FromIBAN && x.OwnerId == userId, cancellationToken);
            if (from.Amount < model.Amount)
                throw new AccountBalanceException(ExceptionTexts.AccountBalance);

            var to = await _repo.GetByPredicateAsync(x => x.IBAN == model.ToIBAN, cancellationToken);
            if (to == null)
                throw new AccountNotFoundException(ExceptionTexts.AccountNotFound);

            if (from.OwnerId == to.OwnerId)
                throw new InvalidTransactionException(ExceptionTexts.InvalidTransactionToAnotherAccount);

            from.Amount -= model.Amount;

            if (from.Currency != to.Currency)
            {
                if (from.Currency != Currencies.GEL)
                {
                    var toGel = await _rateService.GetRateAsync(from.Currency, cancellationToken);
                    model.Amount = model.Amount * toGel.RateFormated * toGel.Quantity;
                }

                var rate = await _rateService.GetRateAsync(to.Currency, cancellationToken);
                model.Amount /= rate.RateFormated * rate.Quantity;
            }

            double comission = model.Amount * 0.01 + 0.5;

            model.Amount -= comission;
            to.Amount += model.Amount;

            var transaction = new Domain.Transactions.Transaction
            {
                FromIBAN = model.FromIBAN,
                ToIBAN = model.ToIBAN,
                SenderId = userId,
                RecieverId = to.OwnerId,
                Amount = model.Amount,
                Currency = to.Currency,
            };

            await _transactionService.AddTransactionAsync(transaction, cancellationToken);

            _repo.UpdateRange(new[] { from, to });
            await _repo.SaveChangesAsync(cancellationToken);

            _repo.CommitTransaction();
        }

        public async Task TransferToOwnAccountAsync(TransferModelRequest model, int userId, CancellationToken cancellationToken)
        {
            _repo.BeginTransaction(IsolationLevel.RepeatableRead);

            var from = await _repo.GetByPredicateAsync(x => x.IBAN == model.FromIBAN && x.OwnerId == userId, cancellationToken) ?? throw new AccountNotFoundException(ExceptionTexts.AccountNotFound);
            if (from.Amount < model.Amount)
                throw new AccountBalanceException(ExceptionTexts.AccountBalance);

            var to = await _repo.GetByPredicateAsync(x => x.IBAN == model.ToIBAN && x.OwnerId == userId, cancellationToken);
            if (to == null)
                throw new AccountNotFoundException(ExceptionTexts.AccountNotFound);

            if (from.OwnerId != to.OwnerId || from.Currency == to.Currency)
                throw new InvalidTransactionException(ExceptionTexts.InvalidTransactionToOwnAccount);

            from.Amount -= model.Amount;

            if (from.Currency != to.Currency)
            {
                if (from.Currency != Currencies.GEL)
                {
                    var toGel = await _rateService.GetRateAsync(from.Currency, cancellationToken);
                    model.Amount = model.Amount * toGel.RateFormated * toGel.Quantity;
                }

                var rate = await _rateService.GetRateAsync(to.Currency, cancellationToken);
                model.Amount /= rate.RateFormated * rate.Quantity;
            }

            to.Amount += model.Amount;

            var transaction = new Domain.Transactions.Transaction
            {
                FromIBAN = model.FromIBAN,
                ToIBAN = model.ToIBAN,
                SenderId = userId,
                RecieverId = to.OwnerId,
                Amount = model.Amount,
                Currency = to.Currency,
                Comission = 0
            };

            await _transactionService.AddTransactionAsync(transaction, cancellationToken);

            _repo.UpdateRange(new[] { from, to });
            await _repo.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            _repo.CommitTransaction();
        }
    }
}