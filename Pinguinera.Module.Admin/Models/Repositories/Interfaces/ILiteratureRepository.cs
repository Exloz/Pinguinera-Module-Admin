using pinguinera_final_module.Models.DataTransferObjects;

namespace pinguinera_final_module.Models.Repositories.Interfaces;

public interface ILiteratureRepository
{
    Task VerifyUniqueTitle(NovelSupplierDTO payLoad);
    Task<List<ItemLiterature>> FindItemsInDb(PurchaseDTO payload);
    Task<bool> PersistNewItemLiterature(ItemLiterature itemLiterature);
}