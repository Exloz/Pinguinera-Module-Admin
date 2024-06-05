namespace pinguinera_final_module.Models.Persistence;

public partial class LibraryItem
{
    public Guid LibraryItemId { get; set; }

    public string Title { get; set; } = null!;

    public string Author { get; set; } = null!;

    public decimal Stock { get; set; }

    public decimal BorrowedQuantity { get; set; }

    public virtual BookLibraryItem? BookLibraryItem { get; set; }

    public virtual NovelLibraryItem? NovelLibraryItem { get; set; }
}
