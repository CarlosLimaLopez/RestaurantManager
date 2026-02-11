using System.ComponentModel.DataAnnotations;

namespace RestaurantManager.MenuSections
{
    using Repositories;
    using Context;
    using Dishes;

    public interface IMenuSectionService
    {
        Task<(MenuSection? menuSection, IEnumerable<ValidationResult> errors)> TryAddDishToMenuSection(Guid menuSectionId, Dish dish);
        Task<(MenuSection? menuSection, IEnumerable<ValidationResult> errors)> TryUpdateMenuSection(Guid menuSectionId, MenuSection updateMenuSection);
        Task<(MenuSection? menuSection, IEnumerable<ValidationResult> errors)> TryRemoveMenuSection(Guid menuSectionId);
    }

    public class MenuSectionService : IMenuSectionService
    {
        private readonly IMenuSectionRepository _menuSectionRepository;
        private readonly IUnitOfWork<RestaurantManagerContext> _unitOfWork;
        
        public MenuSectionService(
            IMenuSectionRepository menuSectionRepository,
            IUnitOfWork<RestaurantManagerContext> unitOfWork
            )
        {
            _menuSectionRepository = menuSectionRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<(MenuSection? menuSection, IEnumerable<ValidationResult> errors)> TryAddDishToMenuSection(Guid menuSectionId, Dish dish)
        {
            var menuSection = await _menuSectionRepository.GetMenuSectionById(menuSectionId);
            if (menuSection == null)
                return (null, []);

            menuSection.Dishes.Add(dish);

            await _unitOfWork.CompleteAsync();

            return (menuSection, []);
        }

        public async Task<(MenuSection? menuSection, IEnumerable<ValidationResult> errors)> TryUpdateMenuSection(Guid menuSectionId, MenuSection updateMenuSection)
        {
            var menuSection = await _menuSectionRepository.GetMenuSectionById(menuSectionId);
            if (menuSection == null)
                return (null, []);

            menuSection.Name = updateMenuSection.Name;
            menuSection.Note = updateMenuSection.Note;
            menuSection.Order = updateMenuSection.Order;

            await _unitOfWork.CompleteAsync();

            return (menuSection, []);
        }

        public async Task<(MenuSection? menuSection, IEnumerable<ValidationResult> errors)> TryRemoveMenuSection(Guid menuSectionId)
        {
            var menuSection = await _menuSectionRepository.GetMenuSectionById(menuSectionId);
            if (menuSection == null)
                return (null, []);

            _menuSectionRepository.RemoveMenuSection(menuSection);

            await _unitOfWork.CompleteAsync();

            return (menuSection, []);
        }
    }
}
