using BankingManagement.Application.Accounts.Requests;
using BankingManagement.Domain.Account;
using BankingManagement.Domain.Enums;

namespace BankingManagement.Application.Infrastructure.Helpers
{
    internal static class TransactionHelper
    {
        internal static Domain.Transactions.Transaction CreateTransactionInstance(Account from, Account to, TransferModelRequest model, double comission, TransactionTypes type)
        {
            var transaction = new Domain.Transactions.Transaction
            {
                FromIBAN = from.IBAN,
                SenderId = from.OwnerId,
                ToIBAN = to.IBAN,
                RecieverId = to.OwnerId,
                Amount = model.Amount,
                Currency = from.Currency,
                Date = DateTime.Now,
                Comission = comission,
                TransactionType = type
            };

            return transaction;
        }
    }
}