using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TaxCalculator.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TaxCalculator.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly string DefaultTaxCalculationType = "1000";

        public IActionResult Index()
        {
            var taxCalculationTypes = GetGetTaxCaculatorTypes();

            var viewModel = new TaxCalculatorViweModel
            {
                TaxCalculationTypes = new SelectList(taxCalculationTypes, "Value", "Text", DefaultTaxCalculationType)
            };

            return View(viewModel);
        }

        [HttpGet]
        public IEnumerable<SelectListItem> GetTaxCaculatorTypes(string searchTerm = "", int pageNumber = 1, int pageSize = 10)
        {
            return GetGetTaxCaculatorTypes();
        }

        private List<SelectListItem> GetGetTaxCaculatorTypes()
        {
            // TODO: these values need to be retrieved via the API, from the client
            return new List<SelectListItem>
            {
                new SelectListItem { Value = "7441", Text = $"{7441} - { TaxCalcType.Progressive.ToString() }" },
                new SelectListItem { Value = "1000", Text = $"{1000} - { TaxCalcType.Progressive.ToString() }" },
                new SelectListItem { Value = "A100", Text = $"A100 - { TaxCalcType.FlatValue.ToString() }" },
                new SelectListItem { Value = "7000", Text = $"{7000} - { TaxCalcType.FlatRate.ToString() }", Selected = true },
            };
        }
    }
}
