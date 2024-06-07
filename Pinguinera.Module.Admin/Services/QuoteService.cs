using cotizaciones.pinguinera.project.Models.Factories.Interfaces;
using pinguinera_final_module.Models.DataTransferObjects;
using pinguinera_final_module.Models.Persistence;
using pinguinera_final_module.Models.Repositories.Interfaces;
using pinguinera_final_module.Services.Interfaces;
using pinguinera_final_module.Services.Mapper;

namespace pinguinera_final_module.Services;

public class QuoteService : IQuoteService
{
    private readonly IQuoteRepository _quoteRepository;
    private readonly ILibraryItemRepository _libraryRepository;
    private readonly IQuoteFactory _quoteFactory;
    private readonly IUserService _userService;
    private readonly ISupplierItemService _supplierItemService;
    private readonly SupplierItemMapper _itemMapper = new();
    private readonly QuoteMapper _quoteMapper = new();

    public QuoteService(IUserService userService,
        IQuoteFactory quoteFactory, IQuoteRepository literatureRepository,
        ISupplierItemService supplierItemService,
        IQuoteRepository quoteRepository, ILibraryItemRepository libraryRepository)
    {
        _userService = userService;
        _quoteFactory = quoteFactory;
        _supplierItemService = supplierItemService;
        _quoteRepository = quoteRepository;
        _libraryRepository = libraryRepository;
    }

    public async Task<QuoteResponseDto> CalculateQuoteValue(QuoteRequestDto payload, Guid supplierId)
    {
        var itemsModelList = await _supplierItemService.GetItemsById(payload);
        var itemsEntityList = itemsModelList
            .Select(x => _itemMapper.MapFromModelToItemEntity(x))
            .ToList();

        var supplier = await _userService.GetUserModelById(supplierId);

        var quoteEntity = _quoteFactory.Create(itemsEntityList, supplier.RegisterAt);
        quoteEntity.CalculateQuoteValue();

        var quoteModel = _quoteMapper.MapToQuoteModel(quoteEntity);

        var tasks = SaveQuoteItemsRegisters(payload, itemsModelList, quoteModel);
        await Task.WhenAll(tasks);

        if (await _quoteRepository.Save(quoteModel) == 0) throw new Exception("Error saving quote");
        var quoteResponseDto = _quoteMapper.MapToQuoteResDto(quoteEntity, quoteModel.QuoteId);

        var itemResDtoList = itemsModelList
            .Select(i =>
            {
                var itemModel = i;
                var itemEntity = itemsEntityList.FirstOrDefault(x => x.Title.Equals(itemModel.Title));
                return _itemMapper.MapToQuoteItemResDto(itemModel, itemEntity);
            }).ToList();

        quoteResponseDto.ItemsList = itemResDtoList;
        return quoteResponseDto;
    }

    public async Task<bool> ProcessSaleConfirmation(Guid quoteId, bool isConfirmed)
    {
        if (!isConfirmed)
        {
            return await CancelTransaction(quoteId);
        }

        var quoteSupplierItems = await _quoteRepository.GetQuoteSupplierItemById(quoteId);
        foreach (var x in quoteSupplierItems)
        {
            var supplierItemModel = x.SupplierItemSupplierItem;
            var libraryItemModel = _itemMapper.MapFromModelToLibraryItem(supplierItemModel, (int)x.Quantity);
            if (await _libraryRepository.Save(libraryItemModel) == 0) return false;
            await _supplierItemService.UpdateStock(supplierItemModel.SupplierItemId, (int)x.Quantity);
        }

        return true;
    }

    private async Task<bool> CancelTransaction(Guid quoteId)
    {
        var quote = await _quoteRepository.GetQuoteById(quoteId);
        if (await _quoteRepository.Delete(quote) == 0) throw new Exception("Error cancelling  the transaction");

        var quoteItems = await _quoteRepository.GetQuoteSupplierItemById(quoteId);
        foreach (var quoteItem in quoteItems)
        {
            if (await _quoteRepository.Delete(quoteItem) == 0) throw new Exception("Error cancelling  the transaction");
        }

        return true;
    }

    private IEnumerable<Task<int>> SaveQuoteItemsRegisters(QuoteRequestDto payload,
        List<SupplierItem> itemsModelList, Quote quoteModel)
    {
        var tasks = payload.ItemIdList.Select(async item =>
        {
            var id = item.Id;
            var itemModel = itemsModelList.FirstOrDefault(i => i.SupplierItemId.Equals(id));
            var quoteSupplierItem = _quoteMapper.MapToQuoteSupplierItem(quoteModel, itemModel, item.Amount);
            var itemSaved = await _quoteRepository.Save(quoteSupplierItem);

            if (itemSaved == 0)
            {
                throw new Exception("Error saving quote item");
            }

            return itemSaved;
        });
        return tasks;
    }
}