namespace pinguinera_final_module.Models.Persistence;

public partial class Admin
{
    public Guid AdminId { get; set; }

    public virtual User AdminNavigation { get; set; } = null!;
}
