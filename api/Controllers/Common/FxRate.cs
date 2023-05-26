
using Microsoft.AspNetCore.Mvc;
using Api.Models;
using Microsoft.Extensions.Logging;

namespace Api.Controllers.Common
{
    [ApiController]
    [Route("api/fxrate")]
    public class FxRateController : ControllerBase
    {
        private static readonly Dictionary<string, Dictionary<string, decimal>> rateMatrix = RateMatrix.Matrix;

        [HttpGet("")]
        public IActionResult GetFxRate(string fromCCY, string convertCCY)
        {

            Console.WriteLine("GetFxRate start fromCCY=" + fromCCY + ";convertCCY=" + convertCCY);

            if (rateMatrix.TryGetValue(fromCCY, out var fromCCYRates))
            {
                if (fromCCYRates.TryGetValue(convertCCY, out var fxRate))
                {
                    return Ok(new { fxRate });
                }
            }

            // If the requested currency pair is not found in the rate matrix, return an error response
            return BadRequest("Invalid currency pair or exchange rate not available.");
        }      
    }
}
