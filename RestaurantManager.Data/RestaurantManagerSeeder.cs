using Microsoft.Extensions.DependencyInjection;

namespace RestaurantManager.Context
{
    using Restaurant;

    public static class RestaurantManagerSeeder
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var context = serviceProvider.GetRequiredService<RestaurantManagerContext>();

            context.Database.Migrate();

            if (context.Restaurants.Any())
                return;

            var restaurant = OneRestaurant();

            context.Restaurants.Add(restaurant);

            context.SaveChanges();
        }

        public static Restaurant OneRestaurant()
        {
            var entrantes = new MenuSection(
                "Entrantes",
                [
                    new Dish("Bruschetta", "6.50", "Pan tostado cubierto con tomate fresco, ajo y albahaca"),
                    new Dish("Calamares Crujientes", "8.90", "Calamares fritos ligeramente, servidos con alioli de limón")
                ],
                "Perfectos para compartir antes del plato principal."
            );

            var platosPrincipales = new MenuSection(
                "Platos Principales",
                [
                    new Dish("Salmón a la Parrilla", "18.50", "Filete de salmón fresco a la parrilla con mantequilla de hierbas"),
                    new Dish("Entrecot de Ternera", "22.00", "Jugoso entrecot de ternera cocinado al punto deseado"),
                    new Dish("Lasaña Vegetariana", "15.00", "Capas de pasta con verduras de temporada y bechamel cremosa")
                ],
                "Todos los platos principales se sirven con una guarnición de verduras asadas."
            );

            var postres = new MenuSection(
                "Postres",
                [
                    new Dish("Coulant de Chocolate", "6.00", "Bizcocho de chocolate caliente con corazón fundido"),
                    new Dish("Tarta de Queso", "5.50", "Clásica tarta de queso cremosa con salsa de frutos rojos")
                ],
                "Postres caseros elaborados a diario."
            );

            var bebidas = new MenuSection(
                "Bebidas",
                [
                    new Dish("Agua Mineral", "2.50", "Agua con gas o sin gas"),
                    new Dish("Vino Tinto de la Casa", "4.00", "Copa de nuestro vino tinto seleccionado")
                ],
                "Pregunta a nuestro personal por la carta de vinos."
            );

            var menu = new Menu(
                [
                    entrantes,
                    platosPrincipales,
                    postres,
                    bebidas
                ],
                "Los precios incluyen IVA. Por favor, informa al personal de cualquier alergia."
            );

            var restaurant = new Restaurant(
                "Casa Umami", 
                "casa-umami", 
                menu,
                "#59151D",
                "Bienvenidos a Casa Umami, donde cada plato es una experiencia culinaria única.",
                "images/logos/casa-umami-logo.png");

            return restaurant;
        }
    }
}
