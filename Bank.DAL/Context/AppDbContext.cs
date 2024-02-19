using Bank.Core.Entities.Account;
using Bank.Core.Entities.Models;
using Bank.DAL.Configurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Bank.DAL.Context
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(SliderCongiguration).Assembly);
            base.OnModelCreating(builder);
        }

        public DbSet<Setting> Settings { get; set; } 
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<BankIcon> BankIcons {get; set; }
        public DbSet<Card> Cards { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CardImage> CardImages { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<CardFeature> CardFeatures { get; set; }
        public DbSet<Currency> Currencies {get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<CardRequest> CardRequests { get; set; }
        public DbSet<Loan> Loans { get; set; }
    }
}
