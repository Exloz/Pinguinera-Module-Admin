using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using pinguinera_final_module.Models.DataTransferObjects;
using pinguinera_final_module.Services.Interfaces;

namespace pinguinera_final_module.Controllers;


[Route("[controller]")]
[ApiController]
public class SupplierItemsController: ControllerBase
{
    private readonly ISupplierItemService _supplierItemService;
    private readonly IValidator<BookRequestDTO> _bookValidator;
    private readonly IValidator<NovelRequestDTO> _novelValidator;

    public SupplierItemsController(ISupplierItemService supplierItemService,
        IValidator<BookRequestDTO> bookValidator, IValidator<NovelRequestDTO> novelValidator)
    {
        _supplierItemService = supplierItemService;
        _bookValidator = bookValidator;
        _novelValidator = novelValidator;
    }

    [HttpPost("AddBook/{supplierId}")]
    public async Task<IActionResult> AddSupplierItem( [FromBody] BookRequestDTO payload, Guid supplierId)
    {
        var validate = await _bookValidator.ValidateAsync(payload);
        if (!validate.IsValid) return StatusCode(StatusCodes.Status400BadRequest, validate.Errors);

        try
        {
            var result = await _supplierItemService.AddSupplierItem(payload, supplierId);
            if (result == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { Error = "Item couldn't be created" });

            }
            return StatusCode(StatusCodes.Status200OK, result);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status400BadRequest, e.Message);
        }
    }
    
    [HttpPost("AddNovel/{supplierId}")]
    public async Task<IActionResult> AddSupplierItem( [FromBody] NovelRequestDTO payload, Guid supplierId)
    {
        var validate = await _novelValidator.ValidateAsync(payload);
        if (!validate.IsValid) return StatusCode(StatusCodes.Status400BadRequest, validate.Errors);

        try
        {
            var result = await _supplierItemService.AddSupplierItem(payload, supplierId);
            if (result == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { Error = "Item couldn't be created" });

            }
            return StatusCode(StatusCodes.Status200OK, result);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status400BadRequest, e.Message);
        }
    }
    
    [HttpGet("GetAll/{supplierId}")]
    public async Task<IActionResult> GetSupplierItems(Guid supplierId)
    {
        try
        {
            var result = await _supplierItemService.GetSupplierItems(supplierId);
            return StatusCode(StatusCodes.Status200OK, result);
        }
        catch (ArgumentException e)
        {
            return StatusCode(StatusCodes.Status503ServiceUnavailable, e.Message);
        }
    }
    
}