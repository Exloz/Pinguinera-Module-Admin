using pinguinera_final_module.Domain.Entities;

namespace cotizaciones.pinguinera.project.Models.Factories.Interfaces;

public interface IQuoteFactory
{
    QuoteEntity Create(List<SupplierItemEntity> publicationsList, DateOnly clientLoyaltyStartDate );
    
    
}