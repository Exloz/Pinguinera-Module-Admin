using pinguinera_final_module.Domain.Entities;
using pinguinera_final_module.Shared.Enums;
using WebApplication1.DTOs;

namespace pinguinera_final_module.Models.DataTransferObjects;

public class QuoteResponseDTO
{
    public Guid QuoteId { get; set; }
    public QuoteType Type { get; set; }
    public double TotalQuoteValue { get; set; }
    public double SeniorityDiscount { get; set; }
    public double QuoteTypeDiscount { get; set; }
    public List<QuoteItemResDTO> ItemsList { get; set; } = [];

}