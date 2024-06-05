namespace pinguinera_final_module.Models.Persistence;

public partial class Reader
{
    public Guid ReaderId { get; set; }

    public virtual ICollection<Loan> Loans { get; set; } = new List<Loan>();

    public virtual User ReaderNavigation { get; set; } = null!;
}
