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
    public class BankIconConfiguration : IEntityTypeConfiguration<BankIcon>
    {
        public void Configure(EntityTypeBuilder<BankIcon> builder)
        {
            builder.Property(x => x.SubTitle).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Title).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Description).IsRequired().HasMaxLength(2000);
            builder.Property(x => x.Icon).IsRequired();
        }
    }
}
