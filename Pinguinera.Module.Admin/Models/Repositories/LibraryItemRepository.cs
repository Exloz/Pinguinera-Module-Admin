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
        if (item.BookLibraryItem != null)
        {
            item.BookLibraryItem.BookLibraryItemId = item.LibraryItemId; // Ensure the FK is set correctly
            await _database.BookLibraryItems.AddAsync(item.BookLibraryItem);
        }

        if (item.NovelLibraryItem != null)
        {
            item.NovelLibraryItem.NovelLibraryItemId = item.LibraryItemId; // Ensure the FK is set correctly
            await _database.NovelLibraryItems.AddAsync(item.NovelLibraryItem);
        }
        
        return await _database.SaveChangesAsync();
    }
}