using BankingManagement.Application.Repositories;
using BankingManagement.Domain.Enums;
using BankingManagement.Domain.Rates;

namespace BankingManagement.Application.Rates
{
    internal class RateService : IRateService
    {
        #region Private Members and CTOR

        private readonly IRepository<Rate> _repo;

        public RateService(IRepository<Rate> repo)
        {
            _repo = repo;
        }

        #endregion Private Members and CTOR

        public async Task<Rate> GetRateAsync(Currencies currency, CancellationToken cancellationToken)
        {
            return await _repo.GetByPredicateAsync(x => x.Code == currency.ToString(), cancellationToken);
        }
    }
}