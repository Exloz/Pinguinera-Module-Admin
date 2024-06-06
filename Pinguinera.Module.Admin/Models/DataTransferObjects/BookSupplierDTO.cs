
using FluentValidation;
using pinguinera_final_module.Shared.Enums;

namespace cotizaciones.pinguinera.project.Models.DTOs;

public struct BookSupplierDTO
{
    public string Title { get; set; }
    public string Author { get; set; }
    public double BasePrice { get; set; }
    public double SellPrice { get; set; }
    public int Stock { get; set; }
    public int Pages { get; set; }
    public string KnowledgeArea { get; set; }
}

// public class ItemValidator: AbstractValidator<NovelSupplierDTO>
// {
//     public ItemValidator()
//     {
//         RuleFor(x => x).NotEmpty();
//         RuleFor(x => x.Title).NotEmpty().MinimumLength(4);
//         RuleFor(x => x.OriginalPrice).NotEmpty().GreaterThan(0);
//         RuleFor(x => x.ItemType).NotNull().IsInEnum();
//
//     }
// }