namespace pinguinera_final_module.Models.Persistence;

public partial class NovelLibraryItem
{
    public Guid NovelLibraryItemId { get; set; }

    public string Genre { get; set; } = null!;

    public decimal SuggestedAge { get; set; }
    public virtual LibraryItem LibraryItem { get; set; } = null!;

}
