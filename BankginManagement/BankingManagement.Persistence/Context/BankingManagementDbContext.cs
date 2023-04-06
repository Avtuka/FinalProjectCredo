using BankingManagement.Domain.Rates;
using Microsoft.EntityFrameworkCore;

namespace BankingManagement.Persistence.Context
{
    public class BankingManagementDbContext : DbContext
    {
        public BankingManagementDbContext(DbContextOptions<BankingManagementDbContext> options) : base(options)
        {
        }

        public DbSet<Rate> Rates { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BankingManagementDbContext).Assembly);
        }
    }
}