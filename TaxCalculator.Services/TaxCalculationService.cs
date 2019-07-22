using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaxCalculator.Core;
using TaxCalculator.Data.enums;
using TaxCalculator.Repositories;

namespace TaxCalculator.Services
{
    public class TaxCalculationService : ITaxCalculationService
    {
        private readonly ITaxCalculationTypeRepository _taxCalculationTypeRepository;
        private readonly ITaxCalculationRepository _taxCalculationRepository;

        private readonly Dictionary<TaxCalcType, ITaxCalculator> _taxCalculators = new Dictionary<TaxCalcType, ITaxCalculator>();

        public TaxCalculationService(ITaxCalculationTypeRepository taxCalculationTypeRepository, ITaxCalculationRepository taxCalculationRepository)
        {
            _taxCalculationTypeRepository = taxCalculationTypeRepository;
            _taxCalculationRepository = taxCalculationRepository;

            if (_taxCalculators.Count == 0)
            {
                _taxCalculators.Add(TaxCalcType.FlatRate, new FlatRateTaxCalclator());
                _taxCalculators.Add(TaxCalcType.FlatValue, new FlatValueTaxCalclator());
                _taxCalculators.Add(TaxCalcType.Progressive, new ProgressiveTaxCalclator());
            }
        }

        public async Task<Tuple<decimal, int>> GetTaxAmount(string postalCode, decimal annualIncome)
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

            // TODO: if null - throw...

            var taxCalcType = (TaxCalcType)Enum.Parse(typeof(TaxCalcType), taxCalculationType.Type);

            var tax = this.GetTax(taxCalcType, annualIncome);

            var calculationResultId = await this.SaveCalculation(taxCalculationType.Id, annualIncome, tax); //TODO: return calculationResultId

            return new Tuple<decimal, int>(tax, calculationResultId);
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

        private async Task<int> SaveCalculation(int taxCalcTypeId, decimal annualIncome, decimal taxDue)
        {
            var taxCalculationResult = new TaxCalculation
            {
                 AnnualIncome = annualIncome,
                 TaxCalculationTypeId = taxCalcTypeId,
                 TaxDue = taxDue
            };

            var result = await _taxCalculationRepository.InsertOrUpdateTaxCalculationAsync(taxCalculationResult);
            return result;
        }
    }
}
