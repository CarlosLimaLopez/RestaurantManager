using Microsoft.Extensions.DependencyInjection;

namespace RestaurantManager.Services
{
    using Restaurants;
    using Menus;
    using MenuSections;
    using Dishes;

	public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IRestaurantService, RestaurantService>();
            services.AddScoped<IMenuService, MenuService>();
            services.AddScoped<IMenuSectionService, MenuSectionService>();
            services.AddScoped<IDishService, DishService>();

			return services;
        }
    }
}
