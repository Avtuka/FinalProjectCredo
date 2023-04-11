namespace BankingManagement.Application.Transaction
{
    public interface ITransactionService
    {
        Task AddTransactionAsync(Domain.Transactions.Transaction transaction, CancellationToken cancellationToken);

        Task<List<Domain.Transactions.Transaction>> GetOneDayTransactionsAsync(string iban, CancellationToken cancellationToken);
    }
}