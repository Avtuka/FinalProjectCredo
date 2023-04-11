using BankingManagement.Domain.Transactions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankingManagement.Persistence.Configuration
{
    internal class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasIndex(x => x.FromIBAN);

            builder.Property(x => x.FromIBAN)
                .IsUnicode(false)
                .HasMaxLength(25);

            builder.HasIndex(x => x.ToIBAN);

            builder.Property(x => x.ToIBAN)
                .IsUnicode(false)
                .IsRequired()
                .HasMaxLength(25);

            builder.HasOne(x => x.Sender).WithMany(x => x.TransactionsSent).HasForeignKey(x => x.SenderId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.Reciever).WithMany(x => x.TransactionsRecieved).HasForeignKey(x => x.RecieverId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}