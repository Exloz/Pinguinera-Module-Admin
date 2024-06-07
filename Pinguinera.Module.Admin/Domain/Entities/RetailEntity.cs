namespace pinguinera_final_module.Domain.Entities;

public class RetailEntity : QuoteEntity
{
    private const double IncrementPercentage = 2;

    public RetailEntity(List<SupplierItemEntity> ItemsList,
        DateOnly clientLoyaltyStartDate) : base(ItemsList,
        clientLoyaltyStartDate)
    {
    }

    public override List<SupplierItemEntity> CalculateItemsFinalPrice()
    {
        ItemsList.ForEach(item => item.CalculateFinalPrice(IncrementPercentage));
        QuoteTypeDiscount = 0;
        
        return ItemsList;
    }

    
}