using System.ComponentModel.DataAnnotations;

namespace RestaurantManager.Restaurants
{
    using Context;
    using Menus;
    using Repositories;
    using RestaurantManager.Dishes;
    using RestaurantManager.MenuSections;

    public interface IRestaurantService
    {
        Task<Restaurant?> GetRestaurant(Guid id);
        Task<Restaurant?> GetRestaurant(string path);
        Task<Restaurant[]> GetRestaurants();
        Task<(Restaurant? restaurant, IEnumerable<ValidationResult> errors)> TryCreateRestaurant(Restaurant restaurant);
        Task<(Restaurant? restaurant, IEnumerable<ValidationResult> errors)> TryUpdateRestaurant(Guid restaurantId, Restaurant updateRestaurant);
        Task<(Restaurant? restaurant, IEnumerable<ValidationResult> errors)> TryDeleteRestaurant(Guid restaurantId);
        Task<(Restaurant? restaurant, IEnumerable<ValidationResult> errors)> TryAddMenuToRestaurant(Guid restaurantId, Menu menu);
        Task<(Restaurant? restaurant, IEnumerable<ValidationResult> errors)> TryDuplicateMenuToDate(Guid restaurantId, Guid menuId, DateOnly newDate);
    }

    public class RestaurantService : IRestaurantService
    {
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IUnitOfWork<RestaurantManagerContext> _unitOfWork;
        
        public RestaurantService(
            IRestaurantRepository restaurantRepository,
            IUnitOfWork<RestaurantManagerContext> unitOfWork
            )
        {
            _restaurantRepository = restaurantRepository;
            _unitOfWork = unitOfWork;
        }

        public Task<Restaurant?> GetRestaurant(Guid id) => _restaurantRepository.GetRestaurantById(id);
        public Task<Restaurant?> GetRestaurant(string path) => _restaurantRepository.GetRestaurantByPath(path);
        public Task<Restaurant[]> GetRestaurants() => _restaurantRepository.GetRestaurants();
        public async Task<(Restaurant? restaurant, IEnumerable<ValidationResult> errors)> TryCreateRestaurant(Restaurant restaurant)
        {
            _restaurantRepository.AddRestaurant(restaurant);

            await _unitOfWork.CompleteAsync();

            return (restaurant, []);
        }
        public async Task<(Restaurant? restaurant, IEnumerable<ValidationResult> errors)> TryUpdateRestaurant(Guid id, Restaurant updateRestaurant)
        {
            var restaurant = await _restaurantRepository.GetRestaurantById(id);
            if (restaurant == null)
                return (null, []);

            restaurant.Name = updateRestaurant.Name;
            restaurant.NameColor = updateRestaurant.NameColor;
            restaurant.Path = updateRestaurant.Path;
            restaurant.Description = updateRestaurant.Description;
            restaurant.DescriptionColor = updateRestaurant.DescriptionColor;
            restaurant.LogoPath = updateRestaurant.LogoPath;

            await _unitOfWork.CompleteAsync();

            return (restaurant, []);
        }

        public async Task<(Restaurant? restaurant, IEnumerable<ValidationResult> errors)> TryDeleteRestaurant(Guid restaurantId)
        {
            var restaurant = await _restaurantRepository.GetRestaurantById(restaurantId);
            if (restaurant == null)
                return (null, []);

            _restaurantRepository.RemoveRestaurant(restaurant);

            await _unitOfWork.CompleteAsync();

            return (restaurant, []);
        }

        public async Task<(Restaurant? restaurant, IEnumerable<ValidationResult> errors)> TryAddMenuToRestaurant(Guid restaurantId, Menu menu)
        {
            var restaurant = await _restaurantRepository.GetRestaurantById(restaurantId);
            if (restaurant == null)
                return (null, []);

            restaurant.AddMenu(menu);

            await _unitOfWork.CompleteAsync();

            return (restaurant, []);
        }

        public async Task<(Restaurant? restaurant, IEnumerable<ValidationResult> errors)> TryDuplicateMenuToDate(Guid restaurantId, Guid menuId, DateOnly newDate)
        {
            var restaurant = await _restaurantRepository.GetRestaurantById(restaurantId);
            if (restaurant == null)
                return (null, []);

            var sourceMenu = restaurant.Menus.FirstOrDefault(m => m.Id == menuId);
            if (sourceMenu == null)
                return (null, []);

            var newMenu = new Menu([], newDate, sourceMenu.Note, sourceMenu.NameSectionColor);

            restaurant.AddMenu(newMenu);

            foreach (var section in sourceMenu.Sections)
            {
                var newSection = new MenuSection(section.Name, section.Order, [], section.Note);

                newMenu.AddMenuSection(newSection);

                foreach (var dish in section.Dishes)
                {
                    var newDish = new Dish(dish.Name, dish.Order, dish.Allergens, dish.Prize, dish.Description);

                    newSection.Dishes.Add(newDish);
                }
            }

            await _unitOfWork.CompleteAsync();

            return (restaurant, []);
        }
    }
}
