using BankingManagement.Application.Repositories;
using BankingManagement.Domain.Enums;
using BankingManagement.Domain.Rates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        #endregion

        public async Task<Rate> GetRateAsync(Currencies currency, CancellationToken cancellationToken)
        {
            return await _repo.GetByPredicateAsync(x => x.Code == currency.ToString(), cancellationToken);
        }
    }


}
