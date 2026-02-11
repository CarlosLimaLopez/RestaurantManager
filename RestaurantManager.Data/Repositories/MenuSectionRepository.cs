namespace RestaurantManager.Repositories
{
    using Context;
    using MenuSections;

    public interface IMenuSectionRepository
    {
        Task<MenuSection?> GetMenuSectionById(Guid menuSectionId);
        void RemoveMenuSection(MenuSection menuSection);
    }

    public class MenuSectionRepository : IMenuSectionRepository
    {
        private readonly RestaurantManagerContext _restaurantManagerContext;

        public MenuSectionRepository(RestaurantManagerContext restaurantManagerContext)
        {
            _restaurantManagerContext = restaurantManagerContext;
        }


        public Task<MenuSection?> GetMenuSectionById(Guid menuSectionId)
            => GetMenuSectionQuery().FirstOrDefaultAsync(r => r.Id == menuSectionId);

        private IQueryable<MenuSection> GetMenuSectionQuery()
            => _restaurantManagerContext.MenuSections.Include(ms => ms.Dishes);

        public void RemoveMenuSection(MenuSection menuSection)
            => _restaurantManagerContext.MenuSections.Remove(menuSection);
           
    }
}
