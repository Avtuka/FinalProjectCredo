﻿using BankingManagement.Domain.Account;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankingManagement.Persistence.Configuration
{
    internal class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasIndex(x => x.IBAN)
                .IsUnique();

            builder.Property(x => x.IBAN)
                .IsUnicode(false)
                .IsRequired()
                .HasMaxLength(25);

            builder.Property(x => x.Amount)
                .IsRequired();

            builder.Property(x => x.Currency)
                .IsRequired();

            builder.HasOne(x => x.Card).WithMany(x => x.Accounts).HasForeignKey(x => x.CardId);
        }
    }
}