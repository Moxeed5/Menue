using Bham_Events.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.X509Certificates;

[ApiController]
[Route("api/[controller]")]
public class MenuController : ControllerBase
{
    private readonly FirebaseService _firebaseService;

    public MenuController(FirebaseService firebaseService)
    {
        _firebaseService = firebaseService;
    }

    //service method for getting all menus

    [HttpGet]
    public async Task<ActionResult<List<Menu>>> GetAllMenus()
    {
        try
        {
            var menus = await _firebaseService.GetAllMenusAsync();
            return Ok(menus);
        }
        catch (Exception ex)
        {
            // Handle the exception appropriately
            return StatusCode(500, "Internal server error: " + ex.Message);
        }
       
    }

    

}
