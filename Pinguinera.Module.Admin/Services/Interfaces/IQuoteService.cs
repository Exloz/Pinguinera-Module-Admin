using pinguinera_final_module.Models.DataTransferObjects;

namespace pinguinera_final_module.Services.Interfaces;

public interface IQuoteService
{
    Task<QuoteResponseDto> CalculateQuoteValue(QuoteRequestDto payload, Guid supplierId);
}