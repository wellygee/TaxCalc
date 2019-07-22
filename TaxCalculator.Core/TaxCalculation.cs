using System;
using System.Collections.Generic;
using System.Text;

namespace TaxCalculator.Core
{
    public class TaxCalculation
    {
        public int Id { get; set; }

        public int TaxCalculationTypeId { get; set; }

        public decimal AnnualIncome { get; set; }

        public decimal TaxDue { get; set; }
    }
}
