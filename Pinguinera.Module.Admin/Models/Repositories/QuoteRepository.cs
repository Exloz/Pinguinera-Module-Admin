using Microsoft.EntityFrameworkCore;
using pinguinera_final_module.Database.Interfaces;
using pinguinera_final_module.Models.Persistence;
using pinguinera_final_module.Models.Repositories.Interfaces;

namespace pinguinera_final_module.Models.Repositories;

public class QuoteRepository: IQuoteRepository
{
    private readonly IDatabase _database;

    public QuoteRepository(IDatabase database)
    {
        _database = database;
    }
    
    
    public async Task<int> Save(Quote quote)
    {
        await _database.Quotes.AddAsync(quote);
        return await _database.SaveChangesAsync();
    }
    
    public async Task<int> Save(QuoteSupplierItem quoteSupplierItem)
    {
        await _database.QuoteSupplierItems.AddAsync(quoteSupplierItem);
        return await _database.SaveChangesAsync();
    }
    
    public async Task<List<SupplierItem>> GetItemsByQuoteId(Guid quoteId)
    {
        var items = await _database.QuoteSupplierItems
            .Where(q => q.QuoteQuoteId.Equals(quoteId))
            .Select(q => q.SupplierItemSupplierItem)
            .ToListAsync();
         
        if (!items.Any())
        {
            throw new ArgumentException("No items found for the provided quote ID.");
        }

        return items;
    }
}