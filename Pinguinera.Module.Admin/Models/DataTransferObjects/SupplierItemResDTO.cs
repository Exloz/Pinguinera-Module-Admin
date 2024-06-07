using pinguinera_final_module.Shared.Enums;

namespace pinguinera_final_module.Models.DataTransferObjects;

public struct SupplierItemResDTO()
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public double SellPrice { get; set; }
    public int Stock { get; set; }
    public ItemType ItemType { get; set; }
}