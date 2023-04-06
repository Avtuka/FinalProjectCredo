using BankingManagement.Domain.Enums;
using BankingManagement.Domain.Rates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingManagement.Application.Rates
{
    public interface IRateService
    {
        Task<Rate> GetRateAsync(Currencies currency, CancellationToken cancellationToken);
    }
}
