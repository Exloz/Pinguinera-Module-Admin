using cotizaciones.pinguinera.project.Database;
using cotizaciones.pinguinera.project.Models.DTOs;
using cotizaciones.pinguinera.project.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace cotizaciones.pinguinera.project.Services;

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