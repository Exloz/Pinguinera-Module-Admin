using pinguinera_final_module.Domain.Entities;
using pinguinera_final_module.Models.DataTransferObjects;
using pinguinera_final_module.Models.Persistence;
using pinguinera_final_module.Models.Repositories.Interfaces;
using pinguinera_final_module.Services.Interfaces;
using pinguinera_final_module.Services.Mapper;
using pinguinera_final_module.Shared.Enums;

namespace pinguinera_final_module.Services;

public class SupplierItemService : ISupplierItemService
{
    private readonly ISupplierItemRepository _supplierItemRepository;
    private readonly SupplierItemMapper _itemMapper = new();
    private readonly IUserService _userService;

    public SupplierItemService(ISupplierItemRepository supplierItemRepository, IUserService userService)
    {
        _supplierItemRepository = supplierItemRepository;
        _userService = userService;
    }

    public async Task<SupplierItemResDTO?> AddSupplierItem(BookRequestDTO payload, Guid supplierId)
    {
        await _supplierItemRepository.VerifyUniqueTitle(payload.Title, supplierId);

        var itemEntity = new SupplierItemEntity(payload.Title, payload.BasePrice, ItemType.BOOK);
        itemEntity.CalculateSellPrice();

        var supplier = await _userService.GetSupplierById(supplierId);
        var itemModel = _itemMapper.MapToItemModel(payload, supplier, itemEntity.SellPrice);
        if (await _supplierItemRepository.Save(itemModel) == 0) return null;

        var responseDto = _itemMapper.MapToSupplierItemResDto(payload, itemModel.SupplierItemId);
        responseDto.SellPrice = itemEntity.SellPrice;

        return responseDto;
    }
    
    public async Task<SupplierItemResDTO?> AddSupplierItem(NovelRequestDTO payload, Guid supplierId)
    {
        await _supplierItemRepository.VerifyUniqueTitle(payload.Title, supplierId);

        var itemEntity = new SupplierItemEntity(payload.Title, payload.BasePrice, ItemType.NOVEL);
        itemEntity.CalculateSellPrice();

        var supplier = await _userService.GetSupplierById(supplierId);
        var itemModel = _itemMapper.MapToItemModel(payload, supplier, itemEntity.SellPrice);
        if (await _supplierItemRepository.Save(itemModel) == 0) return null;

        var responseDto = _itemMapper.MapToSupplierItemResDto(payload, itemModel.SupplierItemId);
        responseDto.SellPrice = itemEntity.SellPrice;

        return responseDto;
    }

    public async Task<List<SupplierItemResDTO>> GetSupplierItems(Guid supplierId)
    {
        var itemsLiteratureList = await _supplierItemRepository.GetItemsBySupplier(supplierId);
        if (itemsLiteratureList is null)
        {
            throw new ArgumentException("There are no literature items registered for this supplier");
        }

        var itemsBySupplier = itemsLiteratureList
            .Select(i => _itemMapper.MapFromModelToItemResDto(i)).ToList();
        
        return itemsBySupplier;
    }

    public async Task<List<SupplierItem>> GetItemsById(QuoteRequestDTO payload)
    {
        List<SupplierItem> itemsList = [];
        foreach (var x in payload.ItemIdList)
        {
            for (var i = 0; i < x.Amount; i++)
            {
                var supplierItem = await _supplierItemRepository.GetItemById(x.Id);
                itemsList.Add(supplierItem);
            }
        }

        itemsList.RemoveAll(item => item == null);
        return itemsList;
    }

    public async Task<SupplierItemResDTO?> UpdateStock(Guid itemId, int quantitySold)
    {
        var supplierItem = await _supplierItemRepository.GetItemById(itemId);
        supplierItem.Stock -= quantitySold;

        if (supplierItem.Stock < 0)
        {
            throw new ArgumentException("Unable to complete the sale due to insufficient stock"); 
        }
        
        if (await _supplierItemRepository.Save(supplierItem) == 0) return null;

        var itemResDto = _itemMapper.MapFromModelToItemResDto(supplierItem);
        return itemResDto;
    }
    
    public async Task<SupplierItem> GetItemById(Guid id)
    {
        var supplierItem = await _supplierItemRepository.GetItemById(id);
        return supplierItem;
    }
    
    
}