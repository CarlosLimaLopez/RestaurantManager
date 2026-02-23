using Microsoft.Extensions.DependencyInjection;

namespace RestaurantManager.Storage
{
	using Services;

    public static class StorageCollectionExtensions
	{
		public static IServiceCollection AddStorage(this IServiceCollection services)
		{
			services.AddScoped<IImageService, ImageService>();
			
			return services;
		}
	}
}
