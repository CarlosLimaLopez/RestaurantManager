namespace RestaurantManager.Repositories
{
    using Context;
    using Restaurant;

    public interface IRestaurantRepository
    {
        Task<Restaurant?> GetRestaurantByPath(string path);
        Task<Restaurant?> GetRestaurantById(Guid id);
        Task<Restaurant[]> GetRestaurants();
    }

    public class RestaurantRepository : IRestaurantRepository
    {
        private readonly RestaurantManagerContext _restaurantManagerContext;

        public RestaurantRepository(RestaurantManagerContext restaurantManagerContext)
        {
            _restaurantManagerContext = restaurantManagerContext;
        }

        public Task<Restaurant?> GetRestaurantByPath(string path)
            => _restaurantManagerContext.Restaurants
                .Include(r => r.Menu)
                    .ThenInclude(m => m.Sections)
                        .ThenInclude(ms => ms.Dishes)
                .FirstOrDefaultAsync(r => r.Path == path);

        public Task<Restaurant?> GetRestaurantById(Guid id) 
            => _restaurantManagerContext.Restaurants
                .Include(r => r.Menu)
                    .ThenInclude(m => m.Sections)
                        .ThenInclude(ms => ms.Dishes)
                .FirstOrDefaultAsync(r => r.Id == id);
        public Task<Restaurant[]> GetRestaurants()
            => _restaurantManagerContext.Restaurants
                .Include(r => r.Menu)
                    .ThenInclude(m => m.Sections)
                        .ThenInclude(ms => ms.Dishes)
                .ToArrayAsync();
    }
}
