using Microsoft.EntityFrameworkCore;
using pinguinera_final_module.Database.Interfaces;
using pinguinera_final_module.Models.Persistence;
using pinguinera_final_module.Models.Repositories.Interfaces;

namespace pinguinera_final_module.Models.Repositories;

public class SupplierItemRepository : ISupplierItemRepository
{
    private readonly IDatabase _database;

    public SupplierItemRepository(IDatabase database)
    {
        _database = database;
    }

    public async Task VerifyUniqueTitle(string title, Guid supplierId)
    {
        var itemDatabase = await _database.SupplierItems
            .Where(i => i.SupplierId.Equals(supplierId))
            .FirstOrDefaultAsync(x => x.Title.ToLower().Equals(title.ToLower()));
            
        if (itemDatabase is not null)
        {
            throw new ArgumentException($"This item's title is already registered in the database");
        }
    }
    
    public async Task<int> Save(SupplierItem item)
    {
        await _database.SupplierItems.AddAsync(item);
        return await _database.SaveChangesAsync();
    }

    public async Task<List<SupplierItem>> GetItemsBySupplier(Guid supplierId)
    {
        var items = await  _database.SupplierItems
            .Where(i => i.SupplierId.Equals(supplierId)).ToListAsync();

        return items;
    }
    
    public async Task<SupplierItem> GetItemById(Guid itemId)
    {
        var item = await _database.SupplierItems
            .FirstOrDefaultAsync(i => i.SupplierItemId.Equals(itemId));
        
        if (item == null) throw new AggregateException("Supplier item not found");

        return item;
    }
    
    
}