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

    public SupplierItemsController(ISupplierItemService supplierItemService)
    {
        _supplierItemService = supplierItemService;
    }

    [HttpPost("AddBook/{supplierId}")]
    public async Task<IActionResult> AddSupplierItem( [FromBody] BookRequestDTO payload, Guid supplierId)
    {
        // var validate = await _itemValidator.ValidateAsync(payload);
        // if (!validate.IsValid) return StatusCode(StatusCodes.Status400BadRequest, validate.Errors);

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
        // var validate = await _itemValidator.ValidateAsync(payload);
        // if (!validate.IsValid) return StatusCode(StatusCodes.Status400BadRequest, validate.Errors);

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
    
    // [HttpGet("GetAll"), Authorize]
    // public async Task<IActionResult> GetSupplierItems()
    // {
    //     try
    //     {
    //         var result = await _literatureService.GetItemLiteratures();
    //         return StatusCode(StatusCodes.Status200OK, result);
    //     }
    //     catch (ArgumentException e)
    //     {
    //         return StatusCode(StatusCodes.Status503ServiceUnavailable, e.Message);
    //     }
    // }
    
}