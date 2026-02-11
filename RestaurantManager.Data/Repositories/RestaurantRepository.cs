namespace RestaurantManager.Repositories
{
    using Context;
    using Restaurants;

    public interface IRestaurantRepository
    {
        Task<Restaurant?> GetRestaurantByPath(string path);
        Task<Restaurant?> GetRestaurantById(Guid id);
        Task<Restaurant[]> GetRestaurants();
        void AddRestaurant(Restaurant restaurant);
        void RemoveRestaurant(Restaurant restaurant);
    }

    public class RestaurantRepository : IRestaurantRepository
    {
        private readonly RestaurantManagerContext _restaurantManagerContext;

        public RestaurantRepository(RestaurantManagerContext restaurantManagerContext)
        {
            _restaurantManagerContext = restaurantManagerContext;
        }

        public Task<Restaurant?> GetRestaurantByPath(string path)
            => GetRestaurantQuery().FirstOrDefaultAsync(r => r.Path == path);

        public Task<Restaurant?> GetRestaurantById(Guid id) 
            => GetRestaurantQuery().FirstOrDefaultAsync(r => r.Id == id);

        public Task<Restaurant[]> GetRestaurants()
            => _restaurantManagerContext.Restaurants.ToArrayAsync();

        public void AddRestaurant(Restaurant restaurant)
            => _restaurantManagerContext.Restaurants.Add(restaurant);

        public void RemoveRestaurant(Restaurant restaurant)
            => _restaurantManagerContext.Restaurants.Remove(restaurant);

        private IQueryable<Restaurant> GetRestaurantQuery()
            => _restaurantManagerContext.Restaurants
                .Include(r => r.Menus)
                    .ThenInclude(m => m.Sections)
                        .ThenInclude(ms => ms.Dishes);
    }
}
