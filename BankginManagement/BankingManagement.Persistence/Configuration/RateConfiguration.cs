using BankingManagement.Domain.Rates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingManagement.Persistence.Configuration
{
    internal class RateConfiguration : IEntityTypeConfiguration<Rate>
    {
        public void Configure(EntityTypeBuilder<Rate> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasIndex(x => x.Code)
                .IsUnique();

        }
    }
}
