using Microsoft.AspNetCore.Mvc;

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
        public async Task<ActionResult> UpdateDish(
            [FromRoute] Guid dishId,
            [FromBody] Dish dish)
        {
            await _dishService.TryUpdateDish(dishId, dish);

            return NoContent();
        }

        [HttpDelete("{dishId:guid}")]
        public async Task<ActionResult> RemoveDish([FromRoute] Guid dishId)
        {
            await _dishService.TryRemoveDish(dishId);

            return NoContent();
        }
    }
}