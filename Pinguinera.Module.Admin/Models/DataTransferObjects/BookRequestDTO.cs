using FluentValidation;

namespace pinguinera_final_module.Models.DataTransferObjects;

public struct BookRequestDTO
{
    public string Title { get; set; }
    public string Author { get; set; }
    public double BasePrice { get; set; }
    public int Quantity { get; set; }
    public int Pages { get; set; }
    public string KnowledgeArea { get; set; }
}

public class BookValidator : AbstractValidator<BookRequestDTO>
{
    public BookValidator()
    {
        RuleFor(x => x).NotEmpty();
        RuleFor(x => x.Title).NotEmpty().MinimumLength(4);
        RuleFor(x => x.Author).NotEmpty().MinimumLength(4);
        RuleFor(x => x.BasePrice).NotEmpty().GreaterThan(0);
        RuleFor(x => x.Quantity).NotEmpty().GreaterThan(0);
        RuleFor(x => x.Pages).NotEmpty().GreaterThan(10);
        RuleFor(x => x.KnowledgeArea).NotEmpty().MinimumLength(4);
    }
}