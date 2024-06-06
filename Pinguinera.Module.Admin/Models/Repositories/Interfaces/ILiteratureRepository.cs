using cotizaciones.pinguinera.project.Models.DTOs;
using cotizaciones.pinguinera.project.Models.Persistence;

namespace cotizaciones.pinguinera.project.Models.Repositories.Interfaces;

public interface ILiteratureRepository
{
    Task VerifyUniqueTitle(NovelSupplierDTO payLoad);
    Task<List<ItemLiterature>> FindItemsInDb(PurchaseDTO payload);
    Task<bool> PersistNewItemLiterature(ItemLiterature itemLiterature);
}