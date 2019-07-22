using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TaxCalculator.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TaxCalculator.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var taxCalculationTypes = new List<SelectListItem>
            {
                new SelectListItem { Value = "7441", Text = $"{7441} - { TaxCalcType.Progressive.ToString() }" },
                new SelectListItem { Value = "1000", Text = $"{1000} - { TaxCalcType.Progressive.ToString() }" },
                new SelectListItem { Value = "A100", Text = $"A100 - { TaxCalcType.FlatValue.ToString() }" },
                new SelectListItem { Value = "7000", Text = $"{7000} - { TaxCalcType.FlatRate.ToString() }", Selected = true },
            };

            var viewModel = new TaxCalculatorViweModel
            {
                TaxCalculationTypes = new SelectList(taxCalculationTypes, "Value", "Text", "1000")
            };

            return View(viewModel);
        }

        [HttpGet]
        public IEnumerable<SelectListItem> GetTaxCaculatorTypes(string searchTerm = "", int pageNumber = 1, int pageSize = 10)
        {
            var taxCalculationTypes = new List<SelectListItem>
            {
                new SelectListItem { Value = "7441", Text = $"{7441} - { TaxCalcType.Progressive.ToString() }" },
                new SelectListItem { Value = "1000", Text = $"{1000} - { TaxCalcType.Progressive.ToString() }" },
                new SelectListItem { Value = "A100", Text = $"A100 - { TaxCalcType.FlatValue.ToString() }" },
                new SelectListItem { Value = "7000", Text = $"{7000} - { TaxCalcType.FlatRate.ToString() }" },
            };

            return taxCalculationTypes;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
