using BankingManagement.Domain.Enums;
using BankingManagement.Domain.Rates;

namespace BankingManagement.Application.Rates
{
    public interface IRateService
    {
        Task<Rate> GetRateAsync(Currencies currency, CancellationToken cancellationToken);
    }
}