using BankingManagement.Domain.Card;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankingManagement.Persistence.Configuration
{
    internal class CardConfiguration : IEntityTypeConfiguration<Card>
    {
        public void Configure(EntityTypeBuilder<Card> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasIndex(x => x.CardNumber)
                .IsUnique();

            builder.Property(x => x.CardNumber)
                .IsRequired()
                .IsUnicode(false);

            builder.Property(x => x.FullName)
                .IsRequired()
                .IsUnicode(true)
                .HasMaxLength(90);

            builder.Property(x => x.ExpirationDate)
                .IsRequired()
                .HasColumnType("DateTime");

            builder.Property(x => x.CVC)
                .IsRequired();

            builder.Property(x => x.PIN)
                .IsRequired();
        }
    }
}