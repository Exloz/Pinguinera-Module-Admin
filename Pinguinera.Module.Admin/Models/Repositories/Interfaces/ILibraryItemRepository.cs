using pinguinera_final_module.Models.Persistence;

namespace pinguinera_final_module.Models.Repositories.Interfaces;

public interface ILibraryItemRepository
{
    Task<int> Save(LibraryItem item);
}