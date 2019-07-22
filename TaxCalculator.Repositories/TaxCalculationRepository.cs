using System;
using System.Threading.Tasks;
using TaxCalculator.Core;
using TaxCalculator.Data;

namespace TaxCalculator.Repositories
{
    public class TaxCalculationRepository : TaxCalcRepository<TaxCalculation>, ITaxCalculationRepository
    {
        public TaxCalculationRepository(ITaxCalcDbContextFactory dbContextFactory) : base(dbContextFactory) { }

        public async Task<int> InsertOrUpdateTaxCalculationAsync(TaxCalculation taxCalculation)
        {
            var entity = await this.AddEntity(taxCalculation);
            return entity.Id;
        }
    }
}
