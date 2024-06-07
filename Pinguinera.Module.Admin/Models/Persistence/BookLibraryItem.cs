namespace pinguinera_final_module.Models.Persistence;

public partial class BookLibraryItem
{
    public Guid BookLibraryItemId { get; set; }

    public decimal Pages { get; set; }

    public string KnowledgeArea { get; set; } = null!;

}
