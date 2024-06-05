namespace pinguinera_final_module.Models.Persistence;

public partial class Loan
{
    public Guid LoanId { get; set; }

    public Guid ReaderId { get; set; }

    public DateOnly CreatedAt { get; set; }

    public DateOnly UpdatedAt { get; set; }

    public string Status { get; set; } = null!;

    public string? Observation { get; set; }

    public DateOnly? PickUpDate { get; set; }

    public virtual Reader Reader { get; set; } = null!;
}
