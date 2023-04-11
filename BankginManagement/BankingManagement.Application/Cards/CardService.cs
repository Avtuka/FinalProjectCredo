using BankingManagement.Application.Cards.Exceptions;
using BankingManagement.Application.Cards.Responses;
using BankingManagement.Application.Infrastructure.Resources;
using BankingManagement.Application.Repositories;
using BankingManagement.Domain.Card;
using Mapster;

namespace BankingManagement.Application.Cards
{
    internal class CardService : ICardService
    {
        #region Private Members and CTOR

        private readonly IRepository<Card> _repo;

        public CardService(IRepository<Card> repo)
        {
            _repo = repo;
        }

        #endregion Private Members and CTOR

        public async Task<bool> CardNumberExistsAsync(string cardNumber, CancellationToken cancellationToken)
        {
            var entity = await _repo.GetByPredicateAsync(x => x.CardNumber == cardNumber, cancellationToken);

            return entity != null;
        }

        public async Task<List<CardResponseModel>> GetCardsAsymc(int userId, CancellationToken cancellationToken)
        {
            var cards = await _repo.GetAllWithIncludeAsync(cancellationToken, x => x.Accounts.FirstOrDefault().OwnerId == userId, x => x.Accounts);

            if (cards == null)
                throw new NoCardsException(ExceptionTexts.NoCards);

            return cards.Adapt<List<CardResponseModel>>();
        }
    }
}