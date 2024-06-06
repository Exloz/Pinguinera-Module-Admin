using pinguinera_final_module.Models.DataTransferObjects;

namespace pinguinera_final_module.Services.Interfaces;

public interface ISupplierItemService
{
    Task<SupplierItemResDTO?> AddSupplierItem(BookRequestDTO payload, Guid supplierId);
}