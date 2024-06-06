using pinguinera_final_module.Models.Persistence;

namespace pinguinera_final_module.Models.Repositories.Interfaces;

public interface ISupplierItemRepository
{
    Task VerifyUniqueTitle(string title, Guid supplierId);
    Task<int> Save(SupplierItem item);
    Task<List<SupplierItem>> GetItemsBySupplier(Guid supplierId);
}