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
    
    public async Task<Quote> GetQuoteById(Guid quoteId)
    {
        var quote = await _database.Quotes.FirstOrDefaultAsync(q => q.QuoteId.Equals(quoteId));
         
        if (quote == null)
        {
            throw new ArgumentException("No quote found for the provided quote ID.");
        }

        return quote;
    }
    
    public async Task<List<QuoteSupplierItem>> GetQuoteSupplierItemById(Guid quoteId)
    {
        var quote = await _database.QuoteSupplierItems
            .Where(q => q.QuoteQuoteId.Equals(quoteId)).ToListAsync();
         
        if (quote == null)
        {
            throw new ArgumentException("No quote found for the provided quote ID.");
        }

        return quote;
    }
    
    public async Task<int> Delete(Quote quote)
    {
         _database.Quotes.Remove(quote);
        return await _database.SaveChangesAsync();
    }
    
    public async Task<int> Delete(QuoteSupplierItem quoteSupplierItem)
    {
        _database.QuoteSupplierItems.Remove(quoteSupplierItem);
        return await _database.SaveChangesAsync();
    }
}