using pinguinera_final_module.Database.Interfaces;
using pinguinera_final_module.Models.Persistence;
using pinguinera_final_module.Models.Repositories.Interfaces;

namespace pinguinera_final_module.Models.Repositories;

public class LibraryItemRepository: ILibraryItemRepository
{
    private readonly IDatabase _database;

    public LibraryItemRepository(IDatabase database)
    {
        _database = database;
    }
    public async Task<int> Save(LibraryItem item)
    {
        await _database.LibraryItems.AddAsync(item);
        return await _database.SaveChangesAsync();
    }
}