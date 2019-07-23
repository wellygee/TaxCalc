using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using TaxCalculator.Services;
using TaxCalculator.Core;
using TaxCalculator.Api.Models;
using Microsoft.AspNetCore.Cors;

namespace TaxCalculator.Api.Controllers.v1
{
    /// <summary>
    /// The Tax Calculator
    /// </summary>
    [Produces("application/json")]
    [Route("api/v1/[controller]")]
    [ApiController]
    [EnableCors("AllOrigins")]    
    public class TaxCalculatorController : ControllerBase
    {
        private readonly ITaxCalculationService _taxCalculationService;

        /// <summary>
        /// The Tax Calculator
        /// </summary>
        public TaxCalculatorController(ITaxCalculationService taxCalculationService)
        {
            _taxCalculationService = taxCalculationService;
        }


        /// <summary>
        /// Retrieves tax calculation 'types'
        /// </summary>
        /// <returns>Tax calculation types</returns>
        [HttpGet("taxCalculationTypes")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<TaxCalculationType>>> GetTaxCalculationTypes()
        {
            var result = await _taxCalculationService.GetTaxCalculationTypes();
            return Ok(result.ToList());
        }

        /// <summary>
        /// Calculates tax
        /// </summary>
        /// <param name="query">Tax calculation request parameters </param>
        /// <returns>The tax due</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> GetTax([FromBody] TaxCalculationQuery query)
        {
            var result = await _taxCalculationService.GetTaxAmount(query.PostalCode, query.AnnualIncome);
            return CreatedAtAction(nameof(GetTax), new { id = result.Item2, taxDue = result.Item1 });
        }
    }
}
