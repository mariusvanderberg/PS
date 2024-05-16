using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PaySpace.Calculator.API.Models;
using PaySpace.Calculator.Data.Models;
using PaySpace.Calculator.Services.Abstractions;
using PaySpace.Calculator.Services.Exceptions;
using PaySpace.Calculator.Services.Helpers;

namespace PaySpace.Calculator.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[Controller]")]
    public sealed class PostalCodeController(
        ILogger<CalculatorController> logger,
        IPostalCodeService postalCodeService,
        IMapper mapper)
        : ControllerBase
    {
        /// <summary>
        /// Get all postal codes
        /// </summary>
        /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
        /// <returns><see cref="PagedList{PostalCode}"/><seealso cref="PostalCodeDto"/></returns>
        [HttpGet]
        public async Task<ActionResult<PagedList<PostalCodeDto>>> GetAsync(CancellationToken cancellationToken)
        {
            try
            {
                var postalCodes = await postalCodeService.GetPostalCodes(cancellationToken);
                var result = PagedList<PostalCode>.Create<PostalCodeDto>(mapper, postalCodes);

                return this.Ok(result);
            }
            catch (CalculatorException e)
            {
                logger.LogError(e, e.Message);

                return this.BadRequest(e.Message);
            }
        }
    }
}