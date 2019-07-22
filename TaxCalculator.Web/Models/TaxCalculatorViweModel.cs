using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaxCalculator.Web.Models
{
    public class TaxCalculatorViweModel
    {
        public TaxCalculatorViweModel()
        {
            this.DefaultSelected = TaxCalcType.FlatValue;
        }
        // public List<TaxCalculationType> TaxCalculationTypes { get; set; }

        public TaxCalcType DefaultSelected { get; private set; }

        public SelectList TaxCalculationTypes { get; set; } = new SelectList(new List<SelectListItem> { });
    }
}
