using BankingManagement.Domain.BaseEntity;
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

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            ChangeTimeStamps();

            return await base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BankingManagementDbContext).Assembly);
        }

        private void ChangeTimeStamps()
        {
            var entities = ChangeTracker.Entries().Where(x => x.Entity is IBaseEntity &&
            (x.State == EntityState.Added || x.State == EntityState.Modified));

            foreach (var entity in entities)
            {
                if (entity.State == EntityState.Added)
                {
                    ((IBaseEntity)entity.Entity).CreatedOn = DateTime.UtcNow;
                }
                ((IBaseEntity)entity.Entity).UpdatedOn = DateTime.UtcNow;
            }
        }
    }
}