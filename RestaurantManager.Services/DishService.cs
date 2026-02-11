using System.ComponentModel.DataAnnotations;

namespace RestaurantManager.Dishes
{
    using Context;
    using Repositories;

    public interface IDishService
    {
        Task<(Dish? Dish, IEnumerable<ValidationResult> errors)> TryUpdateDish(Guid DishId, Dish updateDish);
        Task<(Dish? Dish, IEnumerable<ValidationResult> errors)> TryRemoveDish(Guid DishId);
    }

    public class DishService : IDishService
    {
        private readonly IDishRepository _dishRepository;
        private readonly IUnitOfWork<RestaurantManagerContext> _unitOfWork;
        
        public DishService(
            IDishRepository dishRepository,
            IUnitOfWork<RestaurantManagerContext> unitOfWork
            )
        {
            _dishRepository = dishRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<(Dish? Dish, IEnumerable<ValidationResult> errors)> TryUpdateDish(Guid dishId, Dish updateDish)
        {
            var dish = await _dishRepository.GetDishById(dishId);
            if (dish == null)
                return (null, []);

            dish.Name = updateDish.Name;
            dish.Order = updateDish.Order;
            dish.Description = updateDish.Description;
            dish.Prize = updateDish.Prize;
            dish.Allergens = updateDish.Allergens;

            await _unitOfWork.CompleteAsync();

            return (dish, []);
        }

        public async Task<(Dish? Dish, IEnumerable<ValidationResult> errors)> TryRemoveDish(Guid dishId)
        {
            var dish = await _dishRepository.GetDishById(dishId);
            if (dish == null)
                return (null, []);

            _dishRepository.RemoveDish(dish);

            await _unitOfWork.CompleteAsync();

            return (dish, []);
        }
    }
}
