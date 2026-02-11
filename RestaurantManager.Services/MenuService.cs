using System.ComponentModel.DataAnnotations;

namespace RestaurantManager.Menus
{
    using Repositories;
    using Context;
    using MenuSections;

    public interface IMenuService
    {
        Task<(Menu? menu, IEnumerable<ValidationResult> errors)> TryUpdateMenu(Guid restaurantId, Menu updateMenu);
        Task<(Menu? menu, IEnumerable<ValidationResult> errors)> TryAddMenuSection(Guid menuId, MenuSection menuSection);
        Task<(Menu? menu, IEnumerable<ValidationResult> errors)> TryRemoveMenu(Guid menuId);
    }

    public class MenuService : IMenuService
    {
        private readonly IMenuRepository _menuRepository;
        private readonly IUnitOfWork<RestaurantManagerContext> _unitOfWork;
        
        public MenuService(
            IMenuRepository menuRepository,
            IUnitOfWork<RestaurantManagerContext> unitOfWork
            )
        {
            _menuRepository = menuRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<(Menu? menu, IEnumerable<ValidationResult> errors)> TryUpdateMenu(Guid menuId, Menu updateMenu)
        {
            var menu = await _menuRepository.GetMenuById(menuId);
            if (menu == null)
                return (null, []);

            menu.Note = updateMenu.Note;
            menu.NameSectionColor = updateMenu.NameSectionColor;

            await _unitOfWork.CompleteAsync();

            return (menu, []);
        }

        public async Task<(Menu? menu, IEnumerable<ValidationResult> errors)> TryRemoveMenu(Guid menuId)
        {
            var menu = await _menuRepository.GetMenuById(menuId);
            if (menu == null)
                return (null, []);

            _menuRepository.RemoveMenu(menu);

            await _unitOfWork.CompleteAsync();

            return (menu, []);
        }

        public async Task<(Menu? menu, IEnumerable<ValidationResult> errors)> TryAddMenuSection(Guid menuId, MenuSection menuSection)
        {
            var menu = await _menuRepository.GetMenuById(menuId);
            if (menu == null)
                return (null, []);

            menu.AddMenuSection(menuSection);

            await _unitOfWork.CompleteAsync();

            return (menu, []);
        }
    }
}
