namespace pinguinera_final_module.Domain.Entities;

public class WholesaleEntity : QuoteEntity
{
    private const int ItemsForWholesale = 10;
    private const double DiscountByPublication = 0.15F;

    public WholesaleEntity(List<SupplierItemEntity> itemsList,
        DateOnly clientLoyaltyStartDate) : base(itemsList,
        clientLoyaltyStartDate)
    {
    }

    public override List<SupplierItemEntity> CalculateItemsFinalPrice()
    {
        ItemsList = ItemsList.OrderByDescending(i => i.SellPrice).ToList();
        SetItemsFinalPrice(ItemsList);

        return ItemsList;
    }

    private void SetItemsFinalPrice(List<SupplierItemEntity> itemsList)
    {
        var totalPctDiscount = DefineTotalPctDiscount(itemsList);
        totalPctDiscount = totalPctDiscount > 100 ? 100 : totalPctDiscount;
        
        itemsList.Take(10).ToList()
            .ForEach(item => item.CalculateFinalPrice(0));

        itemsList.Skip(10).ToList()
            .ForEach(item => item.CalculateFinalPrice(totalPctDiscount));

        QuoteTypeDiscount = Math.Round(totalPctDiscount, 2);
    }

    private static double DefineTotalPctDiscount(List<SupplierItemEntity> itemsList)
    {
        var totalPctDiscount =
            (itemsList.Count - ItemsForWholesale) * DiscountByPublication;
        return totalPctDiscount;
    }

}