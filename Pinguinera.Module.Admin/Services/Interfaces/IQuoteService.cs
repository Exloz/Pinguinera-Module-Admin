using pinguinera_final_module.Models.DataTransferObjects;

namespace pinguinera_final_module.Services.Interfaces;

public interface IQuoteService
{
    Task<QuoteResponseDTO> CalculateQuoteValue(QuoteRequestDTO payload, Guid supplierId);
    Task<bool> ProcessSaleConfirmation(Guid quoteId, bool isConfirmed);
}