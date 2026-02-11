using Microsoft.AspNetCore.Mvc;

namespace RestaurantManager.Menus
{
    using MenuSections;

    [Route("api/[controller]")]
    [ApiController]
    public class MenusController : ControllerBase
    {
        private readonly IMenuService _menuService;

        public MenusController(IMenuService menuService)
        {
            _menuService = menuService;
        }

        [HttpPut("{menuId:guid}")]
        public async Task<ActionResult> UpdateMenu(
            [FromRoute] Guid menuId,
            [FromBody] Menu menu)
        {
            await _menuService.TryUpdateMenu(menuId, menu);

            return NoContent();
        }

        [HttpDelete("{menuId:guid}")]
        public async Task<ActionResult> RemoveMenu([FromRoute] Guid menuId)
        {
            await _menuService.TryRemoveMenu(menuId);

            return NoContent();
        }

        [HttpPost("{menuId:guid}/sections")]
        public async Task<ActionResult> CreateMenuSection(
            [FromRoute] Guid menuId,
            [FromBody] MenuSection menuSection)
        {
            await _menuService.TryAddMenuSection(menuId, menuSection);

            return Created();
        }
    }
}