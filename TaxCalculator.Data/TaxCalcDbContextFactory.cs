using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;

namespace TaxCalculator.Data
{
    public class TaxCalcDbContextFactory : ITaxCalcDbContextFactory, IDisposable
    {
        public TaxCalcDbContextFactory(IOptions<DbSettings> settings)
        {
            var options = new DbContextOptionsBuilder<TaxCalcDbContext>().UseSqlServer(settings.Value.DbConnectionString).Options;
            DbContext = new TaxCalcDbContext(options);
        }

        ~TaxCalcDbContextFactory()
        {
            Dispose();
        }

        public TaxCalcDbContext DbContext { get; private set; }


        public void Dispose()
        {
            DbContext?.Dispose();
        }
    }
}