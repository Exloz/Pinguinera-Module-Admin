using cotizaciones.pinguinera.project.Models.Factories.Interfaces;
using pinguinera_final_module.Domain.Entities;

namespace cotizaciones.pinguinera.project.Models.Factories;

public class QuoteFactory: IQuoteFactory
{
    public QuoteEntity Create(List<SupplierItemEntity> publicationsList, DateOnly clientLoyaltyStartDate )
    {
        const int minItemsForWholesale = 10;
        
        if (publicationsList.Count <= minItemsForWholesale)
        {
            return new RetailEntity(publicationsList, clientLoyaltyStartDate);
        }
        
        return new WholesaleEntity(publicationsList, clientLoyaltyStartDate);
    }

   
}