using Microsoft.Extensions.DependencyInjection;

namespace RestaurantManager.Services
{
    using Restaurant;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IRestaurantService, RestaurantService>();

            return services;
        }
    }
}
