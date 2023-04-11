using BankingManagement.Application.Infrastructure.Resources;
using BankingManagement.Application.Repositories;
using BankingManagement.Application.Transaction.Exceptions;

namespace BankingManagement.Application.Transaction
{
    internal class TransactionService : ITransactionService
    {
        #region Private Members and CTOR

        private readonly IRepository<Domain.Transactions.Transaction> _repo;

        public TransactionService(IRepository<Domain.Transactions.Transaction> repo)
        {
            _repo = repo;
        }

        #endregion Private Members and CTOR

        public async Task AddTransactionAsync(Domain.Transactions.Transaction transaction, CancellationToken cancellationToken)
        {
            if (transaction == null) throw new EmptyTransactionException(ExceptionTexts.TransactionNull);

            await _repo.AddAsync(transaction, cancellationToken).ConfigureAwait(false);
            await _repo.SaveChangesAsync(cancellationToken);
        }

        public async Task<List<Domain.Transactions.Transaction>> GetOneDayTransactionsAsync(string iban, CancellationToken cancellationToken)
        {
            var transactions = await _repo.GetAllAsync(x => x.TransactionType == Domain.Enums.TransactionTypes.Withdraw && x.FromIBAN == iban, cancellationToken);

            return transactions;
        }
    }
}