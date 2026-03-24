using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantManager.Restaurants;
using RestaurantManager.Services;

namespace RestaurantManager.Dishes
{
    [Route("api/[controller]")]
    [ApiController]
    public class DishesController : ControllerBase
    {
        private readonly IDishService _dishService;

        public DishesController(IDishService dishService)
        {
            _dishService = dishService;
        }

        [HttpPut("{dishId:guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> UpdateDish(
            [FromRoute] Guid dishId,
            [FromBody] Dish dish)
        {
            await _dishService.TryUpdateDish(dishId, dish);

            return NoContent();
        }

        [HttpDelete("{dishId:guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> RemoveDish([FromRoute] Guid dishId)
        {
            await _dishService.TryRemoveDish(dishId);

            return NoContent();
        }

        [HttpPost("{dishId:guid}/image")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<object>> AddDishImage([FromRoute] Guid dishId, IFormFile file)
        {
            var (dish, errors) = await _dishService.TryAddImage(dishId, file);
            if (dish == null)
                return NotFound();

            if (errors.Any())
                return UnprocessableEntity(errors);

            return Created();
        }

        [HttpGet("{dishId:guid}/image")]
        public async Task<ActionResult> GetDishImage([FromRoute] Guid dishId)
        {
            var (fileResponse, errors) = await _dishService.TryGetDishImageById(dishId);
            if (fileResponse == null)
                return NotFound();

            if (errors.Any())
                return UnprocessableEntity(errors);

            return File(fileResponse.Value.Image, fileResponse.Value.ContentType);
        }

        [HttpDelete("{dishId:guid}/image")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteDishImage([FromRoute] Guid dishId)
        {
            var (dish, errors) = await _dishService.TryRemoveImage(dishId);
            if (dish == null)
                return NotFound();

            if (errors.Any())
                return UnprocessableEntity(errors);

            return NoContent();
        }
    }
}
