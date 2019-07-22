using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaxCalculator.Core;
using TaxCalculator.Data;

namespace TaxCalculator.Repositories
{
    public interface ITaxCalculationTypeRepository
    {
        Task<List<TaxCalculationType>> GetTaxCalculationTypesAsync();
        Task<TaxCalculationType> GetTaxCalculationTypeAsync();
        Task InsertOrUpdateTaxCalculationTypeAsync(TaxCalculationType type);
    }
}
