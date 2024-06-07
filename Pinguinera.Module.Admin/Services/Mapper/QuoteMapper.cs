using pinguinera_final_module.Domain.Entities;
using pinguinera_final_module.Models.DataTransferObjects;
using pinguinera_final_module.Models.Persistence;
using pinguinera_final_module.Shared.Enums;

namespace pinguinera_final_module.Services.Mapper;

public class QuoteMapper
{
    public QuoteResponseDTO MapToQuoteResDto(QuoteEntity quote, Guid quoteId)
    {
        var type = quote is RetailEntity ? QuoteType.RETAIL : QuoteType.WHOLESALE;
        return new QuoteResponseDTO
        {
            QuoteId = quoteId,
            Type = type,
            TotalQuoteValue = quote.TotalQuoteValue,
            SeniorityDiscount = quote.SeniorityDiscount,
            QuoteTypeDiscount = quote.QuoteTypeDiscount,

        };

    }
    
    public Quote MapToQuoteModel(QuoteEntity quote)
    {
        var quoteId = Guid.NewGuid();
        var type = quote is RetailEntity ? QuoteType.RETAIL : QuoteType.WHOLESALE;
        var retailOverPrice = quote is RetailEntity ? 2 : 0;
        return new Quote
        {
            QuoteId = quoteId,
            Type = type.ToString(),
            CreatedAt = DateOnly.FromDateTime(DateTime.Today),
            TotalPrice = quote.TotalQuoteValue,
            RetailOverPrice = retailOverPrice,
            SeniorityDiscount = quote.SeniorityDiscount,
            TypeDiscount = quote.QuoteTypeDiscount,
        };
    }
    
    public QuoteSupplierItem MapToQuoteSupplierItem(Quote quoteModel, SupplierItem itemModel, int quantity)
    {
        return new QuoteSupplierItem
        {
            QuoteQuoteId = quoteModel.QuoteId,
            SupplierItemSupplierItemId = itemModel.SupplierItemId,
            Quantity = quantity,
            QuoteQuote = quoteModel,
            SupplierItemSupplierItem = itemModel,
        };
    }
    
    
}