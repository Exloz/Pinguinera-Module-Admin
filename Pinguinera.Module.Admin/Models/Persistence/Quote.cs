namespace pinguinera_final_module.Models.Persistence;

public partial class Quote
{
    public Guid QuoteId { get; set; }

    public DateOnly CreatedAt { get; set; }

    public string Type { get; set; } = null!;

    public double TotalPrice { get; set; }

    public double? RetailOverPrice { get; set; }

    public double? SeniorityDiscount { get; set; }

    public double? TypeDiscount { get; set; }
}
