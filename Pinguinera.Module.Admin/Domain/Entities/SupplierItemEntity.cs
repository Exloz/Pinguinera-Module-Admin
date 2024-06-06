using pinguinera_final_module.Shared.Enums;

namespace cotizaciones.pinguinera.project.Models.Entities;

public abstract class SupplierItemEntity
{
    public string Title { get; set; }
    public double BasePrice { get; set; }
    public double SellPrice { get; set; }
    public double FinalPrice { get; set; }
    public double PriceIncrement { get; set; }
    public double PriceDiscount { get; set; }
    public ItemType Type { get; set; }

    
    protected SupplierItemEntity(string title, double basePrice, ItemType type)
    {
        Title = title;
        BasePrice = basePrice;
        Type = type;
    }

    public SupplierItemEntity(string title, double basePrice, double sellPrice)
    {
        
        Title = title;
        BasePrice = basePrice;
        SellPrice = sellPrice;
    }

    public double CalculateSellPrice()
    {
        var incrementByType = new Dictionary<ItemType, double>
        {
            { ItemType.BOOK, 0.333 },
            { ItemType.NOVEL, 1 },
        };

        var check = incrementByType.TryGetValue(Type, out var increaseInBasePrice);
        if (!check) throw new ArgumentException("Item Type is not valid");
        
        SellPrice = BasePrice * (1 + increaseInBasePrice);
        SellPrice = Math.Round(SellPrice, 2);
        return SellPrice;
    }
    
    public void CalculateFinalPrice(double adjustment)
    {
        FinalPrice = SellPrice* (1 + adjustment / 100.0);
        FinalPrice = Math.Round(FinalPrice, 2);

        PriceIncrement = adjustment > 0 ? adjustment : 0;
        PriceDiscount = adjustment > 0 ? 0 : -adjustment;
    }
}