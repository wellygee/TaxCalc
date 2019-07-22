using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TaxCalculator.Core;

namespace TaxCalculator.Services
{
    public interface ITaxCalculationService
    {

        Task<IEnumerable<TaxCalculationType>> GetTaxCalculationTypes();

        Task<Tuple<decimal, int>> GetTaxAmount(string postalCode, decimal annualIncome);

    }
}
