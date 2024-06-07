using pinguinera_final_module.Models.Persistence;

namespace pinguinera_final_module.Models.Repositories.Interfaces;

public interface IQuoteRepository
{
    Task<int> Save(Quote quote);
    Task<int> Save(QuoteSupplierItem quoteSupplierItem);
}