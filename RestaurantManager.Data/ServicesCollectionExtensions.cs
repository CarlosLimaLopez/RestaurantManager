using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace RestaurantManager.Data
{
    using Context;
    using Repositories;

    public static class ServicesCollectionExtensions
    {
        public static IServiceCollection AddRestaurantManagerContext( 
            this IServiceCollection services, 
            IConfiguration configuration)
        {
            services.AddDbContext<RestaurantManagerContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("Database")));

            //services.AddDbContext<RestaurantManagerContext>(options =>
            //    options.UseSqlite(configuration.GetConnectionString("Database")));

            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IRestaurantRepository, RestaurantRepository>();
            services.AddScoped<IMenuRepository, MenuRepository>();
            services.AddScoped<IMenuSectionRepository, MenuSectionRepository>();
            services.AddScoped<IDishRepository, DishRepository>();

            return services;
        }

        public static IServiceCollection AddUnitOfWork(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork<RestaurantManagerContext>, UnitOfWork<RestaurantManagerContext>>();

            return services;
        }
    }
}
