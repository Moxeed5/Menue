using Bham_Events.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.X509Certificates;

[ApiController]
[Route("api/[controller]")]
public class MenuItemsController : ControllerBase
{
    private readonly FirebaseService _firebaseService;

    public MenuItemsController(FirebaseService firebaseService)
    {
        _firebaseService = firebaseService;
    }

    [HttpGet]
    public async Task<ActionResult<List<MenuItem>>> GetAllMenuItems()
    {
        try
        {
            var menuItems = await _firebaseService.GetAllMenuItemsAsync();
            return Ok(menuItems);
        }
        catch (Exception ex)
        {
            // Consider logging the exception here
            return StatusCode(500, "Internal server error: " + ex.Message);
        }
    }

    [HttpPost("{restaurantId}")]
    public async Task<IActionResult> AddMenuItem(string restaurantId, [FromBody] MenuItem menuItem)
    {
        if (menuItem == null || string.IsNullOrWhiteSpace(restaurantId))
        {
            return BadRequest("Invalid restaurant ID or menu item data.");
        }

        try
        {
            // Extract properties from menuItem and pass them to AddMenuItemAsync
            await _firebaseService.AddMenuItemAsync(restaurantId, menuItem.Name, menuItem.Description, menuItem.Price);
            return Ok(menuItem);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal server error: " + ex.Message);
        }
    }



}