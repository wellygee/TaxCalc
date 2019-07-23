using System.Threading.Tasks;
using TaxCalculator.Core;

namespace TaxCalculator.Repositories
{
    public interface ITaxCalculationRepository
    {
        Task<int> InsertOrUpdateTaxCalculationAsync(TaxCalculation taxCalculation);
    }
}
