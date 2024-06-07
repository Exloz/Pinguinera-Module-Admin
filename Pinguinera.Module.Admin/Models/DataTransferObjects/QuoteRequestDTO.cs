using cotizaciones.pinguinera.project.Models.DTOs;
using FluentValidation;

namespace pinguinera_final_module.Models.DataTransferObjects;

public class QuoteRequestDTO
{
    public List<QuoteItemReqDTO> ItemIdList { get; set; }
}

public class QuoteValidator : AbstractValidator<QuoteRequestDTO>
{
    public QuoteValidator()
    {
        RuleFor(x => x).NotEmpty();
        RuleForEach(x => x.ItemIdList).NotEmpty()
            .ChildRules(item =>
            {
                item.RuleFor(i => i.Id).NotEmpty();
                item.RuleFor(i => i.Amount).NotEmpty().GreaterThan(0);
            });
    }
}