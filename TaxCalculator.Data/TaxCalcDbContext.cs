using Microsoft.EntityFrameworkCore;
using TaxCalculator.Core;

namespace TaxCalculator.Data
{
    public class TaxCalcDbContext : DbContext
    {
        public TaxCalcDbContext(DbContextOptions<TaxCalcDbContext> options) : base(options){ }

        public virtual DbSet<TaxCalculationType> TaxCalculationType { get; set; }

        public virtual DbSet<TaxCalculation> TaxCalculation { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TaxCalculationType>(entity =>
            {
                //TODO: add entity configuration
            });

            modelBuilder.Entity<TaxCalculationType>(entity =>
            {
                //TODO: add entity configuration
            });
        }
    }
}
