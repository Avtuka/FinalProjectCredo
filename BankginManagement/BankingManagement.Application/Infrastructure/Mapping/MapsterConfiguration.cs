using BankingManagement.Application.Accounts.Responses;
using BankingManagement.Application.Cards.Responses;
using BankingManagement.Domain.Account;
using Mapster;
using Microsoft.Extensions.DependencyInjection;

namespace BankingManagement.Application.Infrastructure.Mapping
{
    public static class MapsterConfiguration
    {
        public static IServiceCollection RegisterMaps(this IServiceCollection services)
        {
            TypeAdapterConfig<Account, CardResponseModel>
                .NewConfig()
                .AfterMapping((src, dst) =>
                {
                    dst.Accounts.Add(src.Adapt<AccountResponseModel>());
                });

            return services;
        }
    }
}