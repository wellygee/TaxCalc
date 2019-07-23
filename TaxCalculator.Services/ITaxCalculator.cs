using System;

namespace TaxCalculator.Services
{
    public interface ITaxCalculator
    {
        decimal CalculateTax(decimal amount);
    }

    public class ProgressiveTaxCalclator : ITaxCalculator
    {
        public decimal CalculateTax(decimal amount)
        {
            var taxBands = new TaxBand[]
            {
                new TaxBand { Lower = 0M, Upper = 8350M, Rate = .10M },
                new TaxBand { Lower = 8351M, Upper = 33950M, Rate = .15M },
                new TaxBand { Lower = 33951M, Upper = 82250M, Rate = .25M },
                new TaxBand { Lower = 82251M, Upper = 171550M, Rate = .28M },
                new TaxBand { Lower = 171551M, Upper = 372950M, Rate = .33M },
                new TaxBand { Lower = 372951M, Upper = decimal.MaxValue, Rate = .35M }
            };

            var salary = amount;
            var taxDue = 0M;

            foreach (var band in taxBands)
            {
                if (salary >= band.Lower)
                {
                    var adjustedLowerBand = band.Lower == 0 ? band.Lower : band.Lower - 1;
                    var taxableAtThisRate = Math.Min(band.Upper - adjustedLowerBand, salary - adjustedLowerBand);
                    var taxThisBand = taxableAtThisRate * band.Rate;
                    taxDue += taxThisBand;
                }
            }

            return taxDue;
        }
    }

    public class FlatValueTaxCalclator : ITaxCalculator
    {
        public decimal CalculateTax(decimal amount)
        {
            var upperBand = 200000;
            int flatRateValue = 10000;
            decimal result;

            if (amount >= upperBand)
            {
                result = flatRateValue;
            }
            else
            {
                result = amount * .05M;
            }

            return result;
        }
    }

    public class FlatRateTaxCalclator : ITaxCalculator
    {
        public decimal CalculateTax(decimal amount)
        {
            return amount * .175M;
        }
    }

    public class TaxBand
    {
        public decimal Lower { get; set; }
        public decimal Upper { get; set; }
        public decimal Rate { get; set; }
    }
}

