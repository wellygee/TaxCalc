using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaxCalculator.Api.Models
{
    public class TaxCalculationQuery
    {
        public string PostalCode { get; set; }

        public decimal AnnualIncome { get; set; }
    }
}
