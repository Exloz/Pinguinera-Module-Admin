namespace pinguinera_final_module.Models.Persistence;

public partial class Supplier
{
    public Guid SupplierId { get; set; }

    public virtual ICollection<SupplierItem> SupplierItems { get; set; } = new List<SupplierItem>();

    public virtual User SupplierNavigation { get; set; } = null!;
}
