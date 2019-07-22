using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaxCalculator.Core;
using TaxCalculator.Data;

namespace TaxCalculator.Repositories
{
    public class TaxCalculationTypeRepository : TaxCalcRepository<TaxCalculationType>, ITaxCalculationTypeRepository
    {
        public TaxCalculationTypeRepository(ITaxCalcDbContextFactory dbContextFactory) : base(dbContextFactory)
        {
        }

        public async Task<TaxCalculationType> GetTaxCalculationTypeAsync()
        {
            var type = await this.DbContext.TaxCalculationType.FirstOrDefaultAsync();
            return type;
        }

        public async Task<List<TaxCalculationType>> GetTaxCalculationTypesAsync()
        {
            var types = await this.DbContext.TaxCalculationType.ToListAsync();
            return types;
        }
    }
}
