namespace pinguinera_final_module.Models.Persistence;

public partial class NovelSupplierItem
{
    public Guid NovelSupplierItemId { get; set; }

    public decimal SuggestedAge { get; set; }

    public string Genre { get; set; } = null!;

}
