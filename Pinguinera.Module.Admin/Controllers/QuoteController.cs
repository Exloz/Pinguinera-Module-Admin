using Microsoft.AspNetCore.Mvc;
using pinguinera_final_module.Models.DataTransferObjects;
using pinguinera_final_module.Services.Interfaces;

namespace pinguinera_final_module.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class QuoteController : ControllerBase
    {
        private readonly IQuoteService _quoteService;

        public QuoteController(IQuoteService quoteService)
        {
            _quoteService = quoteService;
        }

        [HttpPost("/{supplierId}")]
        public async Task<IActionResult> CalculateQuoteValue([FromBody] QuoteRequestDto payload,
            Guid supplierId)
        {
            // var validate = await _itemValidator.ValidateAsync(payload);
            // if (!validate.IsValid) return StatusCode(StatusCodes.Status400BadRequest, validate.Errors);
            try
            {
                var result = await _quoteService.CalculateQuoteValue(payload, supplierId);
                return StatusCode(StatusCodes.Status200OK, result);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status400BadRequest, e.Message);
            }
        }
    }
}