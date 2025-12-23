using System.IO;
using System.Net.WebSockets;
using Microsoft.AspNetCore.Mvc;

namespace RestaurantManager.Restaurant
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantsController : ControllerBase
    {
        private readonly IRestaurantService _restaurantService;

        public RestaurantsController(IRestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }

        [HttpGet]
        public async Task<ActionResult<Restaurant[]>> GetRestaurants()
        {
            var restaurant = await _restaurantService.GetRestaurants();

            return Ok(restaurant);
        }

        [HttpGet("{identifier}")]
        public async Task<ActionResult<Restaurant>> GetRestaurant([FromRoute] string identifier)
        {
            Restaurant? restaurant;

            if (Guid.TryParse(identifier, out var guid))
                restaurant = await _restaurantService.GetRestaurant(guid);
            else
                restaurant = await _restaurantService.GetRestaurant(identifier);
            
            return Ok(restaurant);
        }
    }
}
