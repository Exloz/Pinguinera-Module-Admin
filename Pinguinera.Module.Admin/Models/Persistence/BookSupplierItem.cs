namespace pinguinera_final_module.Models.Persistence;

public partial class BookSupplierItem
{
    public Guid BookSuplierItemId { get; set; }

    public string KnowledgeArea { get; set; } = null!;

    public decimal Pages { get; set; }

}
