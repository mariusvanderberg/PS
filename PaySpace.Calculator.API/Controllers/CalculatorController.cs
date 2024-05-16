using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PaySpace.Calculator.API.Common;
using PaySpace.Calculator.API.Models;
using PaySpace.Calculator.Data.Models;
using PaySpace.Calculator.Services.Abstractions;
using PaySpace.Calculator.Services.Exceptions;
using PaySpace.Calculator.Services.Helpers;
using PaySpace.Calculator.Services.Models;
using PaySpace.Calculator.Services.Models.Dto;

namespace PaySpace.Calculator.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[Controller]")]
    public sealed class CalculatorController(
        ILogger<CalculatorController> logger,
        IHistoryService historyService,
        ICalculatorService calculatorService,
        IMapper mapper)
        : ControllerBase
    {
        /// <summary>
        /// Calculate income tax based on postal code and income
        /// </summary>
        /// <param name="request"><see cref="CalculateRequest"/></param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
        /// <returns><see cref="CalculateResult"/></returns>
        [HttpPost("calculate-tax")]
        public async Task<ActionResult<CalculateResult>> CalculateAsync(CalculateRequest request, CancellationToken cancellationToken)
        {
            try
            {
                // Utlize calculatorService to handle tax calculation
                var result = await calculatorService.CalculateTaxAsync(
                    mapper.Map<CalculateRequestDto>(request),
                    cancellationToken);
                
                // Add history
                await historyService.AddAsync(new CalculatorHistory
                {
                    Tax = result.Tax,
                    Calculator = result.Calculator,
                    PostalCode = request.PostalCode ?? Constants.UNKNOWN,
                    Income = request.Income
                }, cancellationToken);

                return this.Ok(mapper.Map<CalculateResultDto>(result));
            }
            catch (CalculatorException e)
            {
                logger.LogError(e, e.Message);
                return this.BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Fetch all history of tax calculations
        /// </summary>
        /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
        /// <returns><see cref="PagedList{CalculatorHistoryDto}"/>. <seealso cref="CalculatorHistoryDto"/></returns>
        [HttpGet("history")]
        public async Task<ActionResult<PagedList<CalculatorHistoryDto>>> History([FromQuery] QueryRequest request, CancellationToken cancellationToken)
        {
            var historyQuery = historyService.GetHistoryQuery(request);
            var result = await PagedList<CalculatorHistory>.CreateAsync<CalculatorHistoryDto>(mapper, historyQuery, request, cancellationToken);

            return this.Ok(result);
        }
    }
}