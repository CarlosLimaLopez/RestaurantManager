using Microsoft.AspNetCore.Mvc;
using RestaurantManager.Dishes;

namespace RestaurantManager.MenuSections
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuSectionsController : ControllerBase
    {
        private readonly IMenuSectionService _menuSectionService;

        public MenuSectionsController(IMenuSectionService menuSectionService)
        {
            _menuSectionService = menuSectionService;
        }

        [HttpPut("{menuSectionId:guid}")]
        public async Task<ActionResult> UpdateMenuSection(
            [FromRoute] Guid menuSectionId,
            [FromBody] MenuSection menuSection)
        {
            await _menuSectionService.TryUpdateMenuSection(menuSectionId, menuSection);

            return NoContent();
        }

        [HttpDelete("{menuSectionId:guid}")]
        public async Task<ActionResult> RemoveMenuSection([FromRoute] Guid menuSectionId)
        {
            await _menuSectionService.TryRemoveMenuSection(menuSectionId);

            return NoContent();
        }

        [HttpPost("{menuSectionId:guid}/dishes")]
        public async Task<ActionResult> CreateDish([FromRoute] Guid menuSectionId, [FromBody] Dish dish)
        {
            await _menuSectionService.TryAddDishToMenuSection(menuSectionId, dish);

            return Created();
        }
    }
}