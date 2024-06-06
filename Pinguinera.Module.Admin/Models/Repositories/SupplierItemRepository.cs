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

    // public async Task<List<SupplierItem>> FindItemsInDb(PurchaseDTO payload)
    // {
    //     List<SupplierItem> itemsLiteratureList = new();
    //     foreach (var x in payload.ItemIdList)
    //     {
    //         for (int i = 0; i < x.Amount; i++)
    //         {
    //             var itemDatabase = await _database.ItemLiterature.FindAsync(x.Id);
    //             itemsLiteratureList.Add(itemDatabase);
    //         }
    //     }
    //
    //     itemsLiteratureList.RemoveAll(item => item == null);
    //     return itemsLiteratureList;
    // }
    
    
}