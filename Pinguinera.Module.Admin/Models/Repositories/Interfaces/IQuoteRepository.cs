using pinguinera_final_module.Models.Persistence;

namespace pinguinera_final_module.Models.Repositories.Interfaces;

public interface IQuoteRepository
{
    Task<int> Save(Quote quote);
    Task<int> Save(QuoteSupplierItem quoteSupplierItem);
    Task<List<SupplierItem>> GetItemsByQuoteId(Guid quoteId);
    Task<Quote> GetQuoteById(Guid quoteId);
    Task<List<QuoteSupplierItem>> GetQuoteSupplierItemById(Guid quoteId);
    Task<int> Delete(Quote quote);
    Task<int> Delete(QuoteSupplierItem quoteSupplierItem);

}