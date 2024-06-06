using pinguinera_final_module.Models.DataTransferObjects;
using pinguinera_final_module.Models.Persistence;
using pinguinera_final_module.Shared.Enums;

namespace pinguinera_final_module.Services.Mapper;

public class SupplierItemMapper
{
    public  SupplierItemResDTO MapToSupplierItemResDto(BookRequestDTO payload)
    {
        return new SupplierItemResDTO
        {
            Title = payload.Title,
            Author = payload.Author,
            Stock = payload.Quantity,
            ItemType = ItemType.BOOK
        };
    }
    
    public  SupplierItemResDTO MapToSupplierItemResDto(NovelRequestDTO payload)
    {
        return new SupplierItemResDTO
        {
            Title = payload.Title,
            Author = payload.Author,
            Stock = payload.Quantity,
            ItemType = ItemType.NOVEL
        };
    }
    
    public SupplierItem MapToItemModel(BookRequestDTO payload, 
        Supplier supplier, double sellPrice)
    {
        var itemId = Guid.NewGuid();
        return new SupplierItem
        {
            SupplierItemId = itemId,
            SupplierId = supplier.SupplierId,
            Title = payload.Title,
            Author = payload.Author,
            BasePrice = payload.BasePrice,
            SellPrice = sellPrice,
            Stock = payload.Quantity,
            BookSupplierItem = MapToBookModel(payload, itemId),
            Supplier = supplier,
        };
    }
    
    public  SupplierItem MapToItemModel(NovelRequestDTO payload, 
        Supplier supplier, double sellPrice)
    {
        var itemId = Guid.NewGuid();
        return new SupplierItem
        {
            SupplierItemId = itemId,
            SupplierId = supplier.SupplierId,
            Title = payload.Title,
            Author = payload.Author,
            BasePrice = payload.BasePrice,
            SellPrice = sellPrice,
            Stock = payload.Quantity,
            NovelSupplierItem = MapToNovelModel(payload, itemId),
            Supplier = supplier,
        };
    }

    private BookSupplierItem MapToBookModel(BookRequestDTO payload, 
        Guid itemId)
    {
        return new BookSupplierItem
        {
            BookSuplierItemId = itemId,
            KnowledgeArea = payload.KnowledgeArea,
            Pages = payload.Pages
        };

    }
    
    private NovelSupplierItem MapToNovelModel(NovelRequestDTO payload, 
        Guid itemId)
    {
        return new NovelSupplierItem
        {
            NovelSupplierItemId = itemId,
            SuggestedAge = payload.SuggestedAge,
            Genre = payload.Genre
        };

    }
    
    public  SupplierItemResDTO MapFromModelToItemResDto(SupplierItem itemModel)
    {
        var type = itemModel.BookSupplierItem is null ? ItemType.NOVEL : ItemType.BOOK;
        return new SupplierItemResDTO
        {
            Title = itemModel.Title,
            Author = itemModel.Author,
            SellPrice = itemModel.SellPrice,
            Stock = (int) Math.Round(itemModel.Stock, 0),
            ItemType = type
        };
    }
}