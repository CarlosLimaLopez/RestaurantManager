namespace RestaurantManager.Repositories
{
    using Context;
    using Dishes;

    public interface IDishRepository
    {
        Task<Dish?> GetDishById(Guid dishId);
        void RemoveDish(Dish dish);
    }

    public class DishRepository : IDishRepository
    {
        private readonly RestaurantManagerContext _restaurantManagerContext;

        public DishRepository(RestaurantManagerContext restaurantManagerContext)
        {
            _restaurantManagerContext = restaurantManagerContext;
        }

        public Task<Dish?> GetDishById(Guid dishId)
            => GetDishQuery().FirstOrDefaultAsync(r => r.Id == dishId);

        public void RemoveDish(Dish dish)
            => _restaurantManagerContext.Dishes.Remove(dish);

        private IQueryable<Dish> GetDishQuery()
            => _restaurantManagerContext.Dishes;
    }
}
