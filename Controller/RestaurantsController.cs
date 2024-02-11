using Bham_Events.DTO.RestaurantDTO;
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
    public async Task<ActionResult<RestaurantReadDto>> AddRestaurant([FromBody] RestaurantCreateDto createDto)
    {
        // Map the DTO to your domain model
        var restaurantToCreate = new Restaurant
        {
            Name = createDto.Name,
            Location = createDto.Location,
            // Menu is intentionally left out or set to null since it's not part of creation DTO
            Menu = null
        };

        // Call the service to add the new restaurant
        var createdRestaurant = await _firebaseService.AddRestaurantAsync(restaurantToCreate);

        // Check if the restaurant was successfully created
        if (createdRestaurant == null)
        {
            return BadRequest("Could not add the restaurant.");
        }

        // Map the domain model back to a read DTO
        var restaurantReadDto = new RestaurantReadDto
        {
            Id = createdRestaurant.Id,
            Name = createdRestaurant.Name,
            Location = createdRestaurant.Location,
            // Populate other fields as necessary
        };

        // Return the created restaurant read DTO with a 201 Created response
        return CreatedAtAction(nameof(GetRestaurant), new { id = restaurantReadDto.Id }, restaurantReadDto);
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
