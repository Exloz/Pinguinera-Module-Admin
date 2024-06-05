namespace pinguinera_final_module.Models.Persistence;

public partial class QuoteSupplierItem
{
    public Guid QuoteQuoteId { get; set; }

    public Guid SupplierItemSupplierItemId { get; set; }

    public decimal Quantity { get; set; }

    public virtual Quote QuoteQuote { get; set; } = null!;

    public virtual SupplierItem SupplierItemSupplierItem { get; set; } = null!;
}
