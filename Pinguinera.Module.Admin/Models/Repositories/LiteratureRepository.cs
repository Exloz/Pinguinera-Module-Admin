using pinguinera_final_module.Models.DataTransferObjects;
using pinguinera_final_module.Models.Repositories.Interfaces;

namespace pinguinera_final_module.Models.Repositories;

public class LiteratureRepository : ILiteratureRepository
{
    private readonly IDatabase _database;

    public LiteratureRepository(IDatabase database)
    {
        _database = database;
    }

    public async Task VerifyUniqueTitle(NovelSupplierDTO payLoad)
    {
        var itemDatabase = await _database.ItemLiterature
            .FirstOrDefaultAsync(x => x.Title.ToLower().Equals(payLoad.Title.ToLower()));
        if (itemDatabase is not null)
        {
            throw new ArgumentException($"This {payLoad.ItemType}'s title is already registered in the database");
        }
    }

    public async Task<List<ItemLiterature>> FindItemsInDb(PurchaseDTO payload)
    {
        List<ItemLiterature> itemsLiteratureList = new();
        foreach (var x in payload.ItemIdList)
        {
            for (int i = 0; i < x.Amount; i++)
            {
                var itemDatabase = await _database.ItemLiterature.FindAsync(x.Id);
                itemsLiteratureList.Add(itemDatabase);
            }
        }

        itemsLiteratureList.RemoveAll(item => item == null);
        return itemsLiteratureList;
    }
    
    public async Task<bool> PersistNewItemLiterature(ItemLiterature itemLiterature)
    {
        await _database.ItemLiterature.AddAsync(itemLiterature);
        return await _database.SaveAsync();
    }
}