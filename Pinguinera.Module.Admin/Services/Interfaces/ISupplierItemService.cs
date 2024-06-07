using pinguinera_final_module.Models.DataTransferObjects;
using pinguinera_final_module.Models.Persistence;

namespace pinguinera_final_module.Services.Interfaces;

public interface ISupplierItemService
{
    Task<SupplierItemResDTO?> AddSupplierItem(BookRequestDTO payload, Guid supplierId);
    Task<SupplierItemResDTO?> AddSupplierItem(NovelRequestDTO payload, Guid supplierId);
    Task<List<SupplierItemResDTO>> GetSupplierItems(Guid supplierId);
    Task<List<SupplierItem>> GetItemsById(QuoteRequestDto payload);
}