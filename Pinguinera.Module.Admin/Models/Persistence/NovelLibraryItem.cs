namespace pinguinera_final_module.Models.Persistence;

public partial class NovelLibraryItem
{
    public Guid NovelLibraryItem1 { get; set; }

    public string Genre { get; set; } = null!;

    public decimal SuggestedAge { get; set; }

    public virtual LibraryItem NovelLibraryItem1Navigation { get; set; } = null!;
}
