using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace TaxCalculator.Data
{
    public interface ITaxCalcDbContextFactory
    {
        TaxCalcDbContext DbContext { get; }
    }
}
