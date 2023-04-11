using BankingManagement.Application.ATM.Requests;
using BankingManagement.Application.Rates;
using BankingManagement.Application.Repositories;
using BankingManagement.Application.Transaction;
using BankingManagement.Domain.Card;
using BankingManagement.Domain.Enums;

namespace BankingManagement.Application.ATM
{
    internal class ATMService : IATMService
    {
        #region Private Members and CTOR

        private readonly IRepository<Card> _cardRepo;
        private readonly ITransactionService _transacionService;
        private readonly IRateService _rateService;

        public ATMService(IRepository<Card> repo, ITransactionService transacionService, IRateService rateServuce)
        {
            _cardRepo = repo;
            _transacionService = transacionService;
            _rateService = rateServuce;
        }

        #endregion Private Members and CTOR

        public async Task<List<KeyValuePair<string, double>>> SeeBalanceAsync(AuthenticateCardRequestModel authenticateModel, CancellationToken cancellationToken)
        {
            var card = await _cardRepo.GetWithIncludeAsync(cancellationToken, x => x.CardNumber == authenticateModel.CardNumber && x.PIN == authenticateModel.PIN, x => x.Accounts);

            if (card == null)
                throw new Exception("Invalid PIN");

            var balance = new List<KeyValuePair<string, double>>();
            foreach (var acc in card.Accounts)
            {
                balance.Add(new KeyValuePair<string, double>(acc.Currency.ToString(), acc.Amount));
            }

            return balance;
        }

        public async Task ChangePin(AuthenticateCardRequestModel authenticateModel, ChangePinRequestModel pinModel, CancellationToken cancellationToken)
        {
            var card = await _cardRepo.GetWithIncludeAsync(cancellationToken, x => x.CardNumber == authenticateModel.CardNumber && x.PIN == authenticateModel.PIN, x => x.Accounts);

            if (card is null)
                throw new Exception("Invalid Pin");

            card.PIN = pinModel.NewPassword;

            _cardRepo.Update(card);
            await _cardRepo.SaveChangesAsync(cancellationToken);
        }

        public async Task WithdrawAsync(AuthenticateCardRequestModel authenticateModel, Currencies currency, double amount, CancellationToken cancellationToken)
        {
            _cardRepo.BeginTransaction(System.Data.IsolationLevel.RepeatableRead);

            if (amount % 5 != 0)
                throw new Exception("You can only withdraw amount which is divisible by 5");

            var card = await _cardRepo.GetWithIncludeAsync(cancellationToken, x => x.CardNumber == authenticateModel.CardNumber && x.PIN == authenticateModel.PIN, x => x.Accounts);

            if (card is null)
                throw new Exception("Invalid Pin");

            if (!card.Accounts.Any(x => x.Currency == currency))
                throw new Exception("Invalid currency");

            var acc = card.Accounts.Where(x => x.Currency == currency).SingleOrDefault();

            double comission = amount * 2 / 100;

            if (acc.Amount < amount + comission)
                throw new Exception("Not enough balance");

            var transactions = await _transacionService.GetOneDayTransactionsAsync(acc.IBAN, cancellationToken);

            double alreadyWithdrawAmount = 0;
            foreach (var trans in transactions)
            {
                switch (trans.Currency)
                {
                    case Currencies.GEL:
                        alreadyWithdrawAmount += trans.Amount;
                        break;

                    case Currencies.USD:
                        double usdRate = (await _rateService.GetRateAsync(Currencies.USD, cancellationToken)).RateFormated;
                        alreadyWithdrawAmount += trans.Amount * usdRate;
                        break;

                    case Currencies.EUR:
                        double eurRate = (await _rateService.GetRateAsync(Currencies.EUR, cancellationToken)).RateFormated;
                        alreadyWithdrawAmount += trans.Amount * eurRate;
                        break;
                }
            }
            double amountInGel = 0;
            switch (currency)
            {
                case Currencies.GEL:
                    amountInGel += amount;
                    break;

                case Currencies.USD:
                    double usdRate = (await _rateService.GetRateAsync(Currencies.USD, cancellationToken)).RateFormated;
                    amountInGel += amount * usdRate;
                    break;

                case Currencies.EUR:
                    double eurRate = (await _rateService.GetRateAsync(Currencies.EUR, cancellationToken)).RateFormated;
                    amountInGel += amount * eurRate;
                    break;
            }

            if (alreadyWithdrawAmount + amountInGel > 10000)
                throw new Exception("Cannot withdraw more than 10000 in one day");

            var transaction = new Domain.Transactions.Transaction
            {
                SenderId = acc.OwnerId,
                ToIBAN = acc.IBAN,
                RecieverId = acc.OwnerId,
                Amount = amount,
                Currency = acc.Currency,
                Date = DateTime.UtcNow,
                Comission = comission,
                TransactionType = TransactionTypes.Withdraw
            };

            acc.Amount -= amount - comission;

            await _cardRepo.SaveChangesAsync(cancellationToken);

            _cardRepo.CommitTransaction();
        }
    }
}