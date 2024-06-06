using pinguinera_final_module.Domain.Entities;
using pinguinera_final_module.Models.DataTransferObjects;
using pinguinera_final_module.Models.Repositories.Interfaces;
using pinguinera_final_module.Services.Interfaces;
using pinguinera_final_module.Services.Mapper;
using pinguinera_final_module.Shared.Enums;

namespace pinguinera_final_module.Services;

public class SupplierItemService : ISupplierItemService
{
    private readonly ISupplierItemRepository _literatureRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly SupplierItemMapper _itemMapper = new();
    private readonly IUserService _userService;

    public SupplierItemService(ISupplierItemRepository literatureRepository,
        IHttpContextAccessor httpContextAccessor, IUserService userService)
    {
        _literatureRepository = literatureRepository;
        _httpContextAccessor = httpContextAccessor;
        _userService = userService;
    }

    public async Task<SupplierItemResDTO?> AddSupplierItem(BookRequestDTO payload, Guid supplierId)
    {
        await _literatureRepository.VerifyUniqueTitle(payload.Title, supplierId);

        var itemEntity = new SupplierItemEntity(payload.Title, payload.BasePrice, ItemType.BOOK);
        itemEntity.CalculateSellPrice();

        var supplier = await _userService.GetSupplierById(supplierId);
        var itemModel = _itemMapper.MapToItemModel(payload, supplier, itemEntity.SellPrice);
        if (await _literatureRepository.Save(itemModel) == 0) return null;

        var responseDto = _itemMapper.MapToSupplierItemResDto(payload);
        responseDto.SellPrice = itemEntity.SellPrice;

        return responseDto;
    }
    
    public async Task<SupplierItemResDTO?> AddSupplierItem(NovelRequestDTO payload, Guid supplierId)
    {
        await _literatureRepository.VerifyUniqueTitle(payload.Title, supplierId);

        var itemEntity = new SupplierItemEntity(payload.Title, payload.BasePrice, ItemType.BOOK);
        itemEntity.CalculateSellPrice();

        var supplier = await _userService.GetSupplierById(supplierId);
        var itemModel = _itemMapper.MapToItemModel(payload, supplier, itemEntity.SellPrice);
        if (await _literatureRepository.Save(itemModel) == 0) return null;

        var responseDto = _itemMapper.MapToSupplierItemResDto(payload);
        responseDto.SellPrice = itemEntity.SellPrice;

        return responseDto;
    }

    // public async Task<List<LiteratureDTOToUi>> GetItemLiteratures()
    // {
    //     var itemsLiteratureList = await _database.ItemLiterature.ToListAsync();
    //     if (itemsLiteratureList is null || itemsLiteratureList.Count == 0)
    //     {
    //         throw new ArgumentException("There are no literature items registered");
    //     }
    //
    //     var literatureDTOList = itemsLiteratureList.Select(item => new LiteratureDTOToUi()
    //     {
    //         ItemId = item.ItemId,
    //         Title = item.Title,
    //         GrossPrice = item.GrossPrice,
    //         ItemType = item.Type
    //     }).ToList();
    //     
    //     return literatureDTOList;
    // }
}