namespace pinguinera_final_module.Models.DataTransferObjects;

public struct QuoteConfirmDTO
{
    public Guid QuoteId { get; set; }
    public bool Confirmed { get; set; }
}