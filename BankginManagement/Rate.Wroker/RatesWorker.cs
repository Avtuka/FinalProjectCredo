using BankingManagement.Domain.Rates;
using BankingManagement.Persistence.Context;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Rate.Wroker
{
    internal class RatesWorker : BackgroundService
    {
        #region Private Members and CTOR
        private readonly ILogger<RatesWorker> _logger;
        private readonly IServiceProvider _serviceProvider;

        public RatesWorker(ILogger<RatesWorker> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }
        #endregion

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope = _serviceProvider.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<BankingManagementDbContext>();

                var url = $"https://nbg.gov.ge/gw/api/ct/monetarypolicy/currencies/ka/json/?date={DateTime.Now:yyyy-MM-dd}";

                var httpClientHandler = new HttpClientHandler();
                httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, SslPolicyErrors) => true;

                using var httpClient = new HttpClient(httpClientHandler);

                try

                {
                    var response = await httpClient.GetAsync(url);

                    var result = JsonConvert.DeserializeObject<RateRequestModel[]>(await response.Content.ReadAsStringAsync())!.FirstOrDefault();

                    var eur = await dbContext.Rates.Where(x => x.Code == "EUR").SingleOrDefaultAsync(stoppingToken);

                    dbContext.Database.BeginTransaction();

                    if (eur == null)
                    {
                        eur = new BankingManagement.Domain.Rates.Rate { Code = "EUR" };
                        await dbContext.AddAsync(eur, stoppingToken);
                        await dbContext.SaveChangesAsync(stoppingToken);
                    }

                    var usd = await dbContext.Rates.Where(x => x.Code == "USD").SingleOrDefaultAsync(stoppingToken);
                    if (usd == null)
                    {
                        usd = new BankingManagement.Domain.Rates.Rate { Code = "USD" };
                        await dbContext.AddAsync(usd, stoppingToken);
                        await dbContext.SaveChangesAsync(stoppingToken);
                    }
                    var newEur = result.Currencies.Where(x => x.Code == "EUR").SingleOrDefault();
                    var newUsd = result.Currencies.Where(x => x.Code == "USD").SingleOrDefault();


                    eur.Quantity = newEur.Quantity;
                    eur.RateFormated = newEur.RateFormated;


                    usd.Quantity = newUsd.Quantity;
                    usd.RateFormated = newUsd.RateFormated;

                    await dbContext.SaveChangesAsync(stoppingToken);

                    dbContext.Database.CommitTransaction();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                }


                await Task.Delay(TimeSpan.FromDays(1), stoppingToken);
            }
        }
    }
}
