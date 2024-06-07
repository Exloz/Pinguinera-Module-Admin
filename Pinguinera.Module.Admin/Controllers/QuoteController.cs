using FluentValidation;
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
        private readonly IValidator<QuoteRequestDTO> _quoteValidator;

        public QuoteController(IQuoteService quoteService,
            IValidator<QuoteRequestDTO> quoteValidator)
        {
            _quoteService = quoteService;
            _quoteValidator = quoteValidator;
        }

        [HttpPost("/{supplierId}")]
        public async Task<IActionResult> CalculateQuoteValue([FromBody] QuoteRequestDTO payload,
            Guid supplierId)
        {
            var validate = await _quoteValidator.ValidateAsync(payload);
            if (!validate.IsValid) return StatusCode(StatusCodes.Status400BadRequest, validate.Errors);
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
        
        [HttpGet("/{quoteId}")]
        public async Task<IActionResult> ProcessSaleConfirmation([FromQuery] bool confirmed,
            Guid quoteId)
        {
            try
            {
                var result = await _quoteService.ProcessSaleConfirmation(quoteId, confirmed);
                return StatusCode(StatusCodes.Status200OK, result);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status400BadRequest, e.Message);
            }
        }
    }
}