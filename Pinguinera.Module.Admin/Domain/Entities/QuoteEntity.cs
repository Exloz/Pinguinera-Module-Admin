namespace pinguinera_final_module.Domain.Entities;

public abstract class QuoteEntity
{
    public List<SupplierItemEntity> ItemsList { get; set; } = new();
    public DateOnly ClientLoyaltyStartDate { get; set; }
    public double TotalQuoteValue { get; set; }
    public double SeniorityDiscount { get; set; }
    public double QuoteTypeDiscount { get; set; }


    public QuoteEntity()
    {
    }

    protected QuoteEntity(List<SupplierItemEntity> itemsList, DateOnly clientLoyaltyStartDate)
    {
        ItemsList = itemsList;
        ClientLoyaltyStartDate = clientLoyaltyStartDate;
    }

    public double CalculateQuoteValue()
    {
        CalculateItemsFinalPrice();
        var itemsSumPrice = ItemsList.Sum(items => items.FinalPrice);
        var loyaltyPctDiscount = DefineLoyaltyDiscount();
        TotalQuoteValue = itemsSumPrice * (1 - (loyaltyPctDiscount / 100.0));
        TotalQuoteValue = Math.Round(TotalQuoteValue, 2);

        return TotalQuoteValue;
    }

    public abstract List<SupplierItemEntity> CalculateItemsFinalPrice();

    private int DefineLoyaltyDiscount()
    {
        var clientLoyaltyYears = DefineClientLoyaltyYears();

        var discountOptions = new Dictionary<int, int>
        {
            { 0, 0 },
            { 1, 12 },
        };

        var discount = discountOptions.GetValueOrDefault(clientLoyaltyYears, 17);
        SeniorityDiscount = discount;
        return discount;
    }

    private int DefineClientLoyaltyYears()
    {
        const float daysNumberInAYear = 265.25F;
        var today = DateOnly.FromDateTime(DateTime.Now);
        var totalDays = today.DayNumber - ClientLoyaltyStartDate.DayNumber;
        var totalYears = totalDays / daysNumberInAYear;
        var yearsRoundToFloor = (int)Math.Floor(totalYears);
        return yearsRoundToFloor;
    }

}