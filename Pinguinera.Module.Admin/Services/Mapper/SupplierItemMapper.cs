using pinguinera_final_module.Domain.Entities;
using pinguinera_final_module.Models.DataTransferObjects;
using pinguinera_final_module.Models.Persistence;
using pinguinera_final_module.Shared.Enums;
using WebApplication1.DTOs;

namespace pinguinera_final_module.Services.Mapper;

public class SupplierItemMapper
{
    public SupplierItemResDTO MapToSupplierItemResDto(BookRequestDTO payload, Guid itemId)
    {
        return new SupplierItemResDTO
        {
            Id = itemId,
            Title = payload.Title,
            Author = payload.Author,
            Stock = payload.Quantity,
            ItemType = ItemType.BOOK
        };
    }

    public SupplierItemResDTO MapToSupplierItemResDto(NovelRequestDTO payload, Guid itemId)
    {
        return new SupplierItemResDTO
        {
            Id = itemId,
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
            BookSupplierItem = MapToBookSupplierModel(payload, itemId),
            Supplier = supplier,
        };
    }

    public SupplierItem MapToItemModel(NovelRequestDTO payload,
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
            NovelSupplierItem = MapToNovelSupplierModel(payload, itemId),
            Supplier = supplier,
        };
    }

    private BookSupplierItem MapToBookSupplierModel(BookRequestDTO payload,
        Guid itemId)
    {
        return new BookSupplierItem
        {
            BookSuplierItemId = itemId,
            KnowledgeArea = payload.KnowledgeArea,
            Pages = payload.Pages
        };
    }

    private NovelSupplierItem MapToNovelSupplierModel(NovelRequestDTO payload,
        Guid itemId)
    {
        return new NovelSupplierItem
        {
            NovelSupplierItemId = itemId,
            SuggestedAge = payload.SuggestedAge,
            Genre = payload.Genre
        };
    }

    public SupplierItemResDTO MapFromModelToItemResDto(SupplierItem itemModel)
    {
        var type = itemModel.BookSupplierItem is null ? ItemType.NOVEL : ItemType.BOOK;
        return new SupplierItemResDTO
        {
            Id = itemModel.SupplierItemId,
            Title = itemModel.Title,
            Author = itemModel.Author,
            SellPrice = itemModel.SellPrice,
            Stock = (int)Math.Round(itemModel.Stock, 0),
            ItemType = type
        };
    }

    public QuoteItemResDTO MapToQuoteItemResDto(SupplierItem itemModel, SupplierItemEntity itemEntity)
    {
        var type = itemModel.BookSupplierItem is null ? ItemType.NOVEL : ItemType.BOOK;
        return new QuoteItemResDTO
        {
            Title = itemModel.Title,
            ItemType = type,
            BasePrice = itemModel.SellPrice,
            PriceIncrement = itemEntity.PriceIncrement,
            PriceDiscount = itemEntity.PriceDiscount,
            Price = itemEntity.FinalPrice
        };
    }

    public SupplierItemEntity MapFromModelToItemEntity(SupplierItem itemModel)
    {
        return new SupplierItemEntity
        (
            itemModel.Title,
            itemModel.BasePrice,
            itemModel.SellPrice
        );
    }

    public LibraryItem MapFromModelToLibraryItem(SupplierItem itemModel, int stock)
    {
        var itemId = Guid.NewGuid();
        var book = itemModel.BookSupplierItem is null ? null : MapToBookLibraryModel(itemModel, itemId);

        var novel = itemModel.NovelSupplierItem is null ? null : MapToNovelLibraryModel(itemModel, itemId);

        return new LibraryItem
        {
            LibraryItemId = itemId,
            Title = itemModel.Title,
            Author = itemModel.Author,
            Stock = stock,
            BorrowedQuantity = 0,
            BookLibraryItem = book,
            NovelLibraryItem = novel
        };
    }

    private BookLibraryItem MapToBookLibraryModel(SupplierItem itemModel,
        Guid itemId)
    {
        return new BookLibraryItem
        {
            BookLibraryItemId = itemId,
            KnowledgeArea = itemModel.BookSupplierItem.KnowledgeArea,
            Pages = itemModel.BookSupplierItem.Pages
        };
    }

    private NovelLibraryItem MapToNovelLibraryModel(SupplierItem itemModel,
        Guid itemId)
    {
        return new NovelLibraryItem
        {
            NovelLibraryItemId = itemId,
            Genre = itemModel.NovelSupplierItem.Genre,
            SuggestedAge = itemModel.NovelSupplierItem.SuggestedAge
        };
    }
}