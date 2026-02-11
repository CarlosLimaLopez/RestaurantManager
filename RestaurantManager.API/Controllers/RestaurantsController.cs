using Microsoft.AspNetCore.Mvc;

namespace RestaurantManager.Restaurants
{
    using Menus;

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
        public async Task<ActionResult<RestaurantSummaryGetResponse[]>> GetRestaurants()
        {
            var restaurant = await _restaurantService.GetRestaurants();

            var mappedRestaurants = restaurant
                .Select(r => r.ToRestaurantSummaryGetResponse())
                .OrderBy(r => r.Name)
                .ToArray();

            return Ok(mappedRestaurants);
        }

        [HttpPost]
        public async Task<ActionResult> CreateRestaurant([FromBody] Restaurant restaurant)
        {
            await _restaurantService.TryCreateRestaurant(restaurant);

            return Created();
        }

        [HttpGet("{identifier}/active-menu")]
        public async Task<ActionResult<RestaurantActiveMenuGetResponse>> GetRestaurantActiveMenu([FromRoute] string identifier)
        {
            Restaurant? restaurant;

            if (Guid.TryParse(identifier, out var guid))
                restaurant = await _restaurantService.GetRestaurant(guid);
            else
                restaurant = await _restaurantService.GetRestaurant(identifier);

            if (restaurant == null)
                return NotFound();
                        
            var mappedRestaurantActiveMenu = restaurant.ToRestaurantActiveMenuGetResponse();
            
            return Ok(mappedRestaurantActiveMenu);
        }

        [HttpPost("{restaurantId:guid}/menu")]
        public async Task<ActionResult> CreateRestaurantMenu([FromRoute] Guid restaurantId, [FromBody] Menu menu)
        {
            await _restaurantService.TryAddMenuToRestaurant(restaurantId, menu);

            return Created();
        }

        [HttpPost("{restaurantId:guid}/menu/{menuId:guid}/duplicate")]
        public async Task<ActionResult> DuplicateMenuToDate(
            [FromRoute] Guid restaurantId,
            [FromRoute] Guid menuId,
            [FromBody] DuplicateMenuRequest request)
        {
            var (restaurant, errors) = await _restaurantService.TryDuplicateMenuToDate(restaurantId, menuId, request.NewDate);

            if (restaurant == null)
                return NotFound();

            return Created();
        }

        [HttpGet("{restaurantId}")]
        public async Task<ActionResult<RestaurantActiveMenuGetResponse>> GetRestaurant([FromRoute] Guid restaurantId)
        {
            var restaurant = await _restaurantService.GetRestaurant(restaurantId);
            if (restaurant == null)
                return NotFound();

            var mappedRestaurant = restaurant.ToRestaurantGetResponse();

            return Ok(mappedRestaurant);
        }

        [HttpPut("{restaurantId:guid}")]
        public async Task<ActionResult> UpdateRestaurant(
            [FromRoute] Guid restaurantId,
            [FromBody] Restaurant updateRestaurant)
        {
            await _restaurantService.TryUpdateRestaurant(restaurantId, updateRestaurant);

            return NoContent();
        }

        [HttpDelete("{restaurantId:guid}")]
        public async Task<ActionResult> RemoveRestaurant(
            [FromRoute] Guid restaurantId)
        {
            //await _restaurantService.TryDeleteRestaurant(restaurantId);

            return NoContent();
        }
    }
}