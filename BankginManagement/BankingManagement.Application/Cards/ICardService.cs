using BankingManagement.Application.Cards.Responses;

namespace BankingManagement.Application.Cards
{
    public interface ICardService
    {
        Task<bool> CardNumberExistsAsync(string cardNumber, CancellationToken cancellationToken);

        Task<List<CardResponseModel>> GetCardsAsymc(int userId, CancellationToken cancellationToken);
    }
}