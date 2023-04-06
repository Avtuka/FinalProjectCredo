﻿using BankingManagement.Domain.Operator;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankingManagement.Persistence.Configuration
{
    internal class OperatorConfiguration : IEntityTypeConfiguration<Operator>
    {
        public void Configure(EntityTypeBuilder<Operator> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.FirstName)
                 .IsRequired()
                 .HasMaxLength(40)
                 .IsUnicode(true);

            builder.Property(x => x.LastName)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(true);

            builder.Property(x => x.PrivateNumber)
                .IsRequired()
                .IsUnicode(false)
                .HasMaxLength(11);

            builder.Property(x => x.DateOfBirth)
                .IsRequired()
                .HasColumnType("DateTime");

            builder.Property(x => x.Email)
                .IsRequired()
                .HasMaxLength(70)
                .IsUnicode(false);

            builder.Property(x => x.PasswordHash)
                .IsRequired()
                .IsUnicode(false)
                .HasMaxLength(512);
        }
    }
}