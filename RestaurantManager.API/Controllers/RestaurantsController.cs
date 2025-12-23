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

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Restaurant>> GetRestaurant([FromRoute] Guid id)
        {
            var restaurant = await _restaurantService.GetRestaurant(id);

            return Ok(restaurant);
        }
    }
}
