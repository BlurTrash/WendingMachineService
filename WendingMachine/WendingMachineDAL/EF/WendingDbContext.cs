using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WendingMachineDAL.Entities;

namespace WendingMachineDAL.EF
{
    public class WendingDbContext : DbContext
    {

        public DbSet<Drink> Drinks { get; set; }
        public DbSet<WendingMachine> WendingMachine { get; set; }
        public DbSet<Coin> Coins { get; set; }

        public WendingDbContext(DbContextOptions<WendingDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<WendingMachine>()
                .HasMany(a => a.Drinks)
                .WithOne(c => c.WendingMachine)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<WendingMachine>()
                .HasMany(a => a.Coins)
                .WithOne(c => c.WendingMachine)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Drink>().Property(u => u.IsActive).HasDefaultValue(true);
            builder.Entity<WendingMachine>().Property(u => u.IsActive).HasDefaultValue(true);
            builder.Entity<Coin>().Property(u => u.IsActive).HasDefaultValue(true);
        }
    }
}
