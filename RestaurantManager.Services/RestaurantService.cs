using System.ComponentModel.DataAnnotations;

namespace RestaurantManager.Restaurant
{
    using Repositories;
    using Context;

    public interface IRestaurantService
    {
        Task<Restaurant?> GetRestaurant(Guid id);
        Task<Restaurant?> GetRestaurant(string path);
        Task<Restaurant[]> GetRestaurants();
    }

    public class RestaurantService : IRestaurantService
    {
        private readonly IRestaurantRepository _restaurantRepository;
        
        public RestaurantService(
            IRestaurantRepository restaurantRepository
            )
        {
            _restaurantRepository = restaurantRepository;
        }

        public Task<Restaurant?> GetRestaurant(Guid id) => _restaurantRepository.GetRestaurantById(id);
        public Task<Restaurant?> GetRestaurant(string path) => _restaurantRepository.GetRestaurantByPath(path);
        public Task<Restaurant[]> GetRestaurants() => _restaurantRepository.GetRestaurants();
    }
}
