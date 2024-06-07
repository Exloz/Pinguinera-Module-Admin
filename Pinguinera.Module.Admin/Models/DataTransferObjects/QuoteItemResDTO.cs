using pinguinera_final_module.Shared.Enums;

namespace WebApplication1.DTOs;

public struct QuoteItemResDTO
{
    public string Title { get; set; }
    public ItemType ItemType { get; set; }
    public double BasePrice { get; set; }
    public double PriceIncrement { get; set;}
    public double PriceDiscount { get; set;}
    public double Price { get; set;}

}