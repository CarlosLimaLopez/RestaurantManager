namespace RestaurantManager.Repositories
{
    using Context;
    using Restaurants;
    using Menus;

    public interface IMenuRepository
    {
        Task<Menu?> GetMenuById(Guid menuId);
        void RemoveMenu(Menu menu);
    }

    public class MenuRepository : IMenuRepository
    {
        private readonly RestaurantManagerContext _restaurantManagerContext;

        public MenuRepository(RestaurantManagerContext restaurantManagerContext)
        {
            _restaurantManagerContext = restaurantManagerContext;
        }

        public Task<Menu?> GetMenuById(Guid menuId)
            => GetMenuQuery().FirstOrDefaultAsync(r => r.Id == menuId);

        public void RemoveMenu(Menu menu) => _restaurantManagerContext.Menus.Remove(menu);

        private IQueryable<Menu> GetMenuQuery()
            => _restaurantManagerContext.Menus
                    .Include(m => m.Sections)
                        .ThenInclude(ms => ms.Dishes);
    }
}
