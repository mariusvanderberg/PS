using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

using PaySpace.Calculator.Web.Models;
using PaySpace.Calculator.Web.Services.Abstractions;
using PaySpace.Calculator.Web.Services.Helpers;
using PaySpace.Calculator.Web.Services.Models;

namespace PaySpace.Calculator.Web.Controllers
{
    public class CalculatorController(ICalculatorHttpService calculatorHttpService) : Controller
    {
        public async Task<IActionResult> Index()
        {
            ViewBag.ShowTax = false; // Default
            var vm = await this.GetCalculatorViewModelAsync();
            return this.View(vm);
        }

        public async Task<IActionResult> History(int? page)
        {
            ViewBag.Page = page ?? 1;
            var request = new QueryRequest() { Page = page ?? 1, SortOrder = "desc" };
            var history = await calculatorHttpService.GetHistoryAsync(request, CancellationToken.None);
            ViewBag.Total = (int)Math.Ceiling((decimal)history.Total / history.PageSize);
            return this.View(new CalculatorHistoryViewModel
            {
                CalculatorHistory = history
            });
        }

        /// <summary>
        /// Calculates tax according to input
        /// </summary>
        /// <param name="request"><see cref="CalculateRequestViewModel"/></param>
        /// <returns>A refreshed screen with tax details if successful</returns>
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public async Task<IActionResult> CalculateTax(CalculateRequestViewModel request)
        {
            if (this.ModelState.IsValid)
            {
                try
                {
                    var response = await calculatorHttpService.CalculateTaxAsync(new CalculateRequest
                    {
                        PostalCode = request.PostalCode,
                        Income = request.Income
                    });

                    ViewBag.Calculator = response?.Calculator;
                    ViewBag.Tax = response?.Tax;
                    ViewBag.ShowTax = true;

                    return await Refresh(request);
                }
                catch (Exception e)
                {
                    ViewBag.ShowTax = false;
                    ModelState.AddModelError(string.Empty, e.Message);
                }
            }

            return await Refresh(request);
        }

        /// <summary>
        /// Refresh the view
        /// </summary>
        /// <param name="request"><see cref="CalculateRequestViewModel"/></param>
        /// <returns><see cref="Task{IActionResult}"/></returns>
        private async Task<IActionResult> Refresh(CalculateRequestViewModel request)
        {
            var vm = await GetCalculatorViewModelAsync(request);
            return View("Index", vm);
        }

        /// <summary>
        /// Populate <see cref="CalculatorViewModel"/> accordingly
        /// </summary>
        /// <param name="request"><see cref="CalculateRequestViewModel"/></param>
        /// <returns><see cref="CalculatorViewModel"/></returns>
        private async Task<CalculatorViewModel> GetCalculatorViewModelAsync(CalculateRequestViewModel? request = null)
        {
            var postalCodes = await calculatorHttpService.GetPostalCodesAsync();

            return new CalculatorViewModel
            {
                PostalCodes = new SelectList(postalCodes?.Data?.Select(x => x.Code)),
                Income = request?.Income ?? 0m,
                PostalCode = request?.PostalCode ?? string.Empty
            };
        }
    }
}