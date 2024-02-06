using Bank.Core.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.DAL.Configurations
{
    public class CurrencyConfiguration : IEntityTypeConfiguration<Currency>
    {
        public void Configure(EntityTypeBuilder<Currency> builder)
        {
            builder.Property(x => x.Title).IsRequired().HasMaxLength(10);
            builder.Property(x => x.SendMoney).IsRequired();
            builder.Property(x => x.RecieveMoney).IsRequired();
            builder.Property(x => x.ImageUrl).IsRequired();
        }
    }
}
