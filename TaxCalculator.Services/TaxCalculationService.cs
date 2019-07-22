using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaxCalculator.Core;
using TaxCalculator.Data;
using TaxCalculator.Repositories;

namespace TaxCalculator.Services
{
    public class TaxCalculationService : ITaxCalculationService
    {
        private readonly ITaxCalculationTypeRepository _taxCalculationTypeRepository;
        private readonly Dictionary<TaxCalcType, ITaxCalculator> _taxCalculators = new Dictionary<TaxCalcType, ITaxCalculator>();

        public TaxCalculationService(ITaxCalculationTypeRepository taxCalculationTypeRepository)
        {
            _taxCalculationTypeRepository = taxCalculationTypeRepository;

            if (_taxCalculators.Count == 0)
            {
                _taxCalculators.Add(TaxCalcType.FlatRate, new FlatRateTaxCalclator());
                _taxCalculators.Add(TaxCalcType.FlatValue, new FlatValueTaxCalclator());
                _taxCalculators.Add(TaxCalcType.Progressive, new ProgressiveTaxCalclator());
            }
        }

        public async Task<decimal> GetTaxAmount(string postalCode, decimal annualIncome)
        {
            if (string.IsNullOrWhiteSpace(postalCode))
            {
                throw new ArgumentNullException(nameof(postalCode));
            }

            if (annualIncome < 0)
            {
                throw new ArgumentException("Annual income must be greater than or equal to 0", nameof(annualIncome));
            }

            var _taxCalculationTypes = await _taxCalculationTypeRepository.GetTaxCalculationTypesAsync();
            var taxCalculationType = _taxCalculationTypes.FirstOrDefault(t => t.PostalCode.Equals(postalCode, StringComparison.CurrentCultureIgnoreCase));

            var taxCalcType = (TaxCalcType)Enum.Parse(typeof(TaxCalcType), taxCalculationType.Type);

            var tax = this.GetTax(taxCalcType, annualIncome);

            // TODO: Save calculation to DB

            return tax;
        }

        public async Task<IEnumerable<TaxCalculationType>> GetTaxCalculationTypes()
        {
            return await _taxCalculationTypeRepository.GetTaxCalculationTypesAsync();
        }

        private decimal GetTax(TaxCalcType taxCalcType, decimal annualIncome)
        {
            var calculator = _taxCalculators[taxCalcType];
            var result = calculator.CalculateTax(annualIncome);
            return result;
        }
    }
}
