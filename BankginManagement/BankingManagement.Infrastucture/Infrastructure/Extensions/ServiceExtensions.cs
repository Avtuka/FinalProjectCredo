using BankingManagement.Application.Repositories;
using BankingManagement.Infrastucture.Repository;
using BankingManagement.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BankingManagement.Infrastucture.Infrastructure.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<BankingManagementDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            return services;
        }
    }
}