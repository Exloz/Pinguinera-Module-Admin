using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace pinguinera_final_module.Controllers;


[Route("[controller]")]
[ApiController]
public class SupplierItemsController
{
    
    [HttpPost("Add"), Authorize]
    public async Task<IActionResult> AddSupplierItem(ItemDTO payload)
    {
        var validate = await _itemValidator.ValidateAsync(payload);
        if (!validate.IsValid) return StatusCode(StatusCodes.Status400BadRequest, validate.Errors);

        try
        {
            var result = await _costingSupplierService.CalculateItemGrossPrice(payload);
            if (result == null)
                return StatusCode(StatusCodes.Status400BadRequest, new { Error = "Item couldn't be created" });
            return StatusCode(StatusCodes.Status200OK, true);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status400BadRequest, e.Message);
        }
    }
    
    [HttpGet("GetAll"), Authorize]
    public async Task<IActionResult> GetSupplierItems()
    {
        try
        {
            var result = await _literatureService.GetItemLiteratures();
            return StatusCode(StatusCodes.Status200OK, result);
        }
        catch (ArgumentException e)
        {
            return StatusCode(StatusCodes.Status503ServiceUnavailable, e.Message);
        }
    }
    
}