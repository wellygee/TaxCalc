using System;
using System.Collections.Generic;
using System.Text;

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
                new TaxBand { Lower = 0M, Upper = 8350M, Rate = 1.10M },
                new TaxBand { Lower = 8351M, Upper = 33950M, Rate = 1.15M },
                new TaxBand { Lower = 33951M, Upper = 82250M, Rate = 1.25M },
                new TaxBand { Lower = 82251M, Upper = 171550M, Rate = 1.28M },
                new TaxBand { Lower = 171551M, Upper = 372950M, Rate = 1.33M },
                new TaxBand { Lower = 372951M, Upper = decimal.MaxValue, Rate = 1.35M }
            };

            var salary = amount;

            var taxDue = 0M;

            foreach (var band in taxBands)
            {
                if (salary > band.Lower)
                {
                    var taxableAtThisRate = Math.Min(band.Upper - band.Lower, salary - band.Lower);
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
                result = amount * 1.05M;
            }

            return result;
        }
    }

    public class FlatRateTaxCalclator : ITaxCalculator
    {
        public decimal CalculateTax(decimal amount)
        {
            return amount * 1.175M;
        }
    }

    public class TaxBand
    {
        public decimal Lower { get; set; }
        public decimal Upper { get; set; }
        public decimal Rate { get; set; }
    }
}

