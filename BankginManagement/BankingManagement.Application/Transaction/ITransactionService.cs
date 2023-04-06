using BankingManagement.Domain.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingManagement.Application.Transaction
{
    public interface ITransactionService
    {
        Task AddTransactionAsync(Domain.Transactions.Transaction transaction, CancellationToken cancellationToken);
    }
}
