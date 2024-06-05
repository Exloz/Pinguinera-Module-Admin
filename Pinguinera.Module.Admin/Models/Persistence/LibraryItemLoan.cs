namespace pinguinera_final_module.Models.Persistence;

public partial class LibraryItemLoan
{
    public Guid LibraryItemLibraryItemId { get; set; }

    public Guid LoanLoanId { get; set; }

    public virtual LibraryItem LibraryItemLibraryItem { get; set; } = null!;

    public virtual Loan LoanLoan { get; set; } = null!;
}
