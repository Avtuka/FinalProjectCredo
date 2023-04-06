using BankingManagement.Application.Accounts.Requests;
using BankingManagement.Application.Accounts.Responses;
using BankingManagement.Application.Rates;
using BankingManagement.Application.Repositories;
using BankingManagement.Application.Transaction;
using BankingManagement.Domain.Account;
using BankingManagement.Domain.Enums;
using BankingManagement.Domain.Transactions;
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
                throw new Exception("Account not found");


            depositAcc.Amount += model.Amount;
            var transaction = new Domain.Transactions.Transaction
            {
                SenderId = userId,
                ToIBAN = model.IBAN,
                RecieverId = userId,
                Amount = model.Amount,
                Currency = model.Currency,
                Date = DateTime.Now,
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
            var accounts = await _repo.GetAllWithIncludeAsync(x => x.OwnerId == userId, x => x.Card);

            if (accounts == null)
                throw new Exception("You do not have any accounts");

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
                throw new Exception("You do not have enough money to transfer");

            var to = await _repo.GetByPredicateAsync(x => x.IBAN == model.ToIBAN, cancellationToken);
            if (to == null)
                throw new Exception("This account does not exists");

            if (from.OwnerId == to.OwnerId)
                throw new Exception("You can use this kind of transaction only for transfering between you and other accounts");

            if (from.Currency != to.Currency)
            {
                if (from.Currency != Currencies.GEL)
                {
                    var toGel = await _rateService.GetRateAsync(from.Currency, cancellationToken);
                    from.Amount *= toGel.RateFormated * toGel.Quantity;
                }

                var rate = await _rateService.GetRateAsync(to.Currency, cancellationToken);
                from.Amount /= rate.RateFormated * rate.Quantity;
            }

            model.Amount -= model.Amount * 0.01 - 0.5;

            from.Amount -= model.Amount;
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

            _repo.UpdateRange(new[] { from, to });
            await _repo.SaveChangesAsync(cancellationToken);

            _repo.CommitTransaction();
        }

        public async Task TransferToOwnAccountAsync(TransferModelRequest model, int userId, CancellationToken cancellationToken)
        {
            _repo.BeginTransaction(IsolationLevel.RepeatableRead);

            var from = await _repo.GetByPredicateAsync(x => x.IBAN == model.FromIBAN && x.OwnerId == userId, cancellationToken).ConfigureAwait(false);
            if (from.Amount < model.Amount)
                throw new Exception("You do not have enough money to transfer");

            var to = await _repo.GetByPredicateAsync(x => x.IBAN == model.ToIBAN && x.OwnerId == userId, cancellationToken).ConfigureAwait(false);
            if (to == null)
                throw new Exception("This account does not exists");

            if (from.OwnerId != to.OwnerId)
                throw new Exception("You can use this kind of transaction only for transfering between your accounts");

            if (from.Currency != to.Currency)
            {
                if (from.Currency != Currencies.GEL)
                {
                    var toGel = await _rateService.GetRateAsync(from.Currency, cancellationToken);
                    from.Amount *= toGel.RateFormated * toGel.Quantity;
                }

                var rate = await _rateService.GetRateAsync(to.Currency, cancellationToken);
                from.Amount /= rate.RateFormated * rate.Quantity;
            }

            from.Amount -= model.Amount;
            to.Amount += model.Amount;

            _repo.UpdateRange(new[] { from, to });
            await _repo.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            _repo.CommitTransaction();
        }
    }
}