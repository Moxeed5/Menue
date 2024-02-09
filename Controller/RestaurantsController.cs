using Bham_Events.Models;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class RestaurantsController : ControllerBase
{
    private readonly FirebaseService _firebaseService;

    public RestaurantsController(FirebaseService firebaseService)
    {
        _firebaseService = firebaseService;
    }

    [HttpPost]
    public async Task<IActionResult> AddRestaurant([FromBody] Restaurant restaurant)
    {
        var createdRestaurant = await _firebaseService.AddRestaurantAsync(restaurant);
        if (createdRestaurant == null)
        {
            return BadRequest("Could not add the restaurant.");
        }
        return CreatedAtAction(nameof(GetRestaurant), new { id = createdRestaurant.Id }, createdRestaurant);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Restaurant>>> GetAllRestaurants()
    {
        var restaurants = await _firebaseService.GetAllRestaurantsAsync();
        return Ok(restaurants);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Restaurant>> GetRestaurant(string id)
    {
        var restaurant = await _firebaseService.GetRestaurantAsync(id);
        if (restaurant == null)
        {
            return NotFound();
        }
        return Ok(restaurant);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateRestaurant(string id, [FromBody] Restaurant restaurant)
    {
        await _firebaseService.UpdateRestaurantAsync(id, restaurant);
        return NoContent(); // Or Ok() if you prefer to return something
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRestaurant(string id)
    {
        await _firebaseService.DeleteRestaurantAsync(id);
        return NoContent();
    }
}
