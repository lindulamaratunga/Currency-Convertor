using Microsoft.EntityFrameworkCore;
using Money.Domain.Models;

namespace Money.Infrastructure.Data
{
    public class CurrencyDbContext : DbContext
    {
        public CurrencyDbContext(DbContextOptions<CurrencyDbContext> options) : base(options)
        {
        }

        public DbSet<CurrencyConversion> CurrencyConversions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure CurrencyConversion entity
            modelBuilder.Entity<CurrencyConversion>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.FromCurrency).IsRequired().HasMaxLength(3);
                entity.Property(e => e.ToCurrency).IsRequired().HasMaxLength(3);
                entity.Property(e => e.Amount).HasConversion<decimal>();
                entity.Property(e => e.ConvertedAmount).HasConversion<decimal>();
                entity.Property(e => e.ExchangeRate).HasConversion<decimal>();
                entity.Property(e => e.DepartmentId).HasMaxLength(100);
            });
        }
    }
}
