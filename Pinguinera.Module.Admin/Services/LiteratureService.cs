using pinguinera_final_module.Services.Interfaces;

namespace pinguinera_final_module.Services;

public class LiteratureService : ILiteratureService
{
    private readonly IDatabase _database;

    public LiteratureService(IDatabase database)
    {
        _database = database;
    }

    public async Task<List<LiteratureDTOToUi>> GetItemLiteratures()
    {
        var itemsLiteratureList = await _database.ItemLiterature.ToListAsync();
        if (itemsLiteratureList is null || itemsLiteratureList.Count == 0)
        {
            throw new ArgumentException("There are no literature items registered");
        }

        var literatureDTOList = itemsLiteratureList.Select(item => new LiteratureDTOToUi()
        {
            ItemId = item.ItemId,
            Title = item.Title,
            GrossPrice = item.GrossPrice,
            ItemType = item.Type
        }).ToList();
        
        return literatureDTOList;
    }
}