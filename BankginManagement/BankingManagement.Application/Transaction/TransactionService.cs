using BankingManagement.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        #endregion

        public async Task AddTransactionAsync(Domain.Transactions.Transaction transaction, CancellationToken cancellationToken)
        {
            if (transaction == null) throw new Exception("Transaction cannot be empty");

            await _repo.AddAsync(transaction, cancellationToken).ConfigureAwait(false);
            await _repo.SaveChangesAsync(cancellationToken);
        }
    }
}
