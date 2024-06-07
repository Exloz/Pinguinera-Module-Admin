using FluentValidation;

namespace pinguinera_final_module.Models.DataTransferObjects;

public struct NovelRequestDTO
{
    public string Title { get; set; }
    public string Author { get; set; }
    public double BasePrice { get; set; }
    public int Quantity { get; set; }
    public int SuggestedAge { get; set; }
    public string Genre { get; set; }
}

public class NovelValidator : AbstractValidator<NovelRequestDTO>
{
    public NovelValidator()
    {
        RuleFor(x => x).NotEmpty();
        RuleFor(x => x.Title).NotEmpty().MinimumLength(4);
        RuleFor(x => x.Author).NotEmpty().MinimumLength(4);
        RuleFor(x => x.BasePrice).NotEmpty().GreaterThan(0);
        RuleFor(x => x.Quantity).NotEmpty().GreaterThan(0);
        RuleFor(x => x.SuggestedAge).NotEmpty().GreaterThan(0);
        RuleFor(x => x.Genre).NotEmpty().MinimumLength(4);
    }
}