using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TaxCalculator.Core;

namespace TaxCalculator.Data
{
    public class TaxCalcDbContext : DbContext
    {
        public TaxCalcDbContext()
        {
        }

        public TaxCalcDbContext(DbContextOptions<TaxCalcDbContext> options) : base(options)
        {
        }

        public virtual DbSet<TaxCalculationType> TaxCalculationType { get; set; }

        public virtual DbSet<TaxCalculation> TaxCalculation { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=TaxCalc;Integrated Security=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TaxCalculationType>(entity =>
            {
                //entity.Property(e => e.Id)
                //    .HasMaxLength(255)
                //    .ValueGeneratedNever();

                //entity.Property(e => e.CountryId)
                //    .IsRequired()
                //    .HasMaxLength(255);

                //entity.Property(e => e.Name)
                //    .IsRequired()
                //    .HasMaxLength(255);
            });

            modelBuilder.Entity<TaxCalculationType>(entity =>
            {
                //entity.Property(e => e.Id)
                //    .HasMaxLength(255)
                //    .ValueGeneratedNever();

                //entity.Property(e => e.CountryId)
                //    .IsRequired()
                //    .HasMaxLength(255);

                //entity.Property(e => e.Name)
                //    .IsRequired()
                //    .HasMaxLength(255);
            });
        }
    }
}
