using cotizaciones.pinguinera.project.Models.DTOs;
using FluentValidation;

namespace pinguinera_final_module.Models.DataTransferObjects;

public class QuoteRequestDto
{
    public List<QuoteItemReqDto> ItemIdList { get; set; }
}

// public class PurchaseValidator : AbstractValidator<PurchaseDTO>
// {
//     public PurchaseValidator()
//     {
//         RuleFor(x => x).NotEmpty();
//         RuleForEach(x => x.ItemIdList).NotEmpty()
//             .ChildRules(item =>
//             {
//                 item.RuleFor(i => i.Id).NotEmpty().GreaterThan(0);
//                 item.RuleFor(i => i.Amount).NotEmpty().GreaterThan(0);
//             });
//     }
// }