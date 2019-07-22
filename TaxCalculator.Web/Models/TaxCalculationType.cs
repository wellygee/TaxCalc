using System;

namespace TaxCalculator.Web.Models
{
    public class TaxCalculationType
    {
        public int Id { get; set; }

        public string PostalCode { get; set; }

        public TaxCalcType Type { get; set; }

        public string TypeDescription { get; set; }

        public string DisplayValue
        {
            get
            {
                return $"{PostalCode} - {TypeDescription}";
            }
        }
    }
}
