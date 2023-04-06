using BankingManagement.Application.Accounts;
using BankingManagement.Application.ATM.Requests;
using BankingManagement.Application.Repositories;
using BankingManagement.Domain.Account;
using BankingManagement.Domain.Card;
using BankingManagement.Domain.Enums;
using BankingManagement.Domain.Rates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace BankingManagement.Application.ATM
{
    internal class ATMService : IATMService
    {
        #region Private Members and CTOR
        private readonly IRepository<Card> _cardRepo;

        public ATMService(IRepository<Card> repo)
        {
            _cardRepo = repo;
        }
        #endregion

        public async Task<List<KeyValuePair<Currencies, double>>> SeeBalanceAsync(AuthenticateCardRequestModel authenticateModel, CancellationToken cancellationToken)
        {
            authenticateModel.CardNumber = authenticateModel.CardNumber.Replace(" ", "");
            var card = await _cardRepo.GetWithIncludeAsync(cancellationToken, x => x.CardNumber == authenticateModel.CardNumber && x.PIN == authenticateModel.PIN, x => x.Accounts);

            if (card == null)
                throw new Exception("Invalid PIN");

            var balance = new List<KeyValuePair<Currencies, double>>();
            foreach (var acc in card.Accounts)
            {
                balance.Add(new KeyValuePair<Currencies, double>(acc.Currency, acc.Amount));
            }

            return balance;
        }

        public async Task ChangePin(AuthenticateCardRequestModel authenticateModel, ChangePinRequestModel pinModel, CancellationToken cancellationToken)
        {
            authenticateModel.CardNumber = authenticateModel.CardNumber.Replace(" ", "");
            var card = await _cardRepo.GetWithIncludeAsync(cancellationToken, x => x.CardNumber == authenticateModel.CardNumber && x.PIN == authenticateModel.PIN, x => x.Accounts);

            if (card is null)
                throw new Exception("Invalid Pin");

            card.PIN = pinModel.NewPassword;

            _cardRepo.Update(card);
            await _cardRepo.SaveChangesAsync(cancellationToken);
        }

        public async Task WithdrawAsync(AuthenticateCardRequestModel authenticateModel, Currencies currency, double amount, CancellationToken cancellationToken)
        {
            if (amount % 5 != 0)
                throw new Exception("You can only withdraw amount which is divisible by 5");

            authenticateModel.CardNumber = authenticateModel.CardNumber.Replace(" ", "");
            var card = await _cardRepo.GetWithIncludeAsync(cancellationToken, x => x.CardNumber == authenticateModel.CardNumber && x.PIN == authenticateModel.PIN, x => x.Accounts);

            if (card is null)
                throw new Exception("Invalid Pin");

            if (!card.Accounts.Any(x => x.Currency == currency))
                throw new Exception("Invalid currency");

            var acc = card.Accounts.Where(x => x.Currency == currency).SingleOrDefault();

            if (acc.Amount < amount)
                throw new Exception("Not enough balance");

            acc.Amount -= amount;

            await _cardRepo.SaveChangesAsync(cancellationToken);
        }

    }
}
