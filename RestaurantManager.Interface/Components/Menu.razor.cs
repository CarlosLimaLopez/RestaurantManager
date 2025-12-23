using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using RestaurantManager.Interface.DTOs.Response;

namespace RestaurantManager.Interface.Components
{
    public partial class Menu: ComponentBase
    {
        [Inject]
        private HttpClient Http { get; set; } = default!;

        public RestaurantResponse? Restaurant { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Restaurant = await GetRestaurant(CancellationToken.None);
        }

        private Task<RestaurantResponse?> GetRestaurant(CancellationToken cancellationToken)
            => Http.GetFromJsonAsync<RestaurantResponse>("restaurants/casa-umami", cancellationToken);
        

        //public RestaurantResponse Restaurant = new()
        //{
        //    Name = "Casa Umami",
        //    NameColor = "#59151D",
        //    Description = "Bienvenidos a Casa Umami, donde cada plato es una experiencia culinaria única.",
        //    LogoPath = "images/logos/casa-umami-logo.png",
        //    Menu = new MenuResponse
        //    {
        //        Note = "Los precios incluyen IVA. Por favor, informa al personal de cualquier alergia.",
        //        Sections =
        //[
        //    new MenuSectionResponse
        //    {
        //        Name = "Entrantes",
        //        Note = "Perfectos para compartir antes del plato principal.",
        //        Dishes =
        //        [
        //            new DishResponse { Name="Bruschetta", Description="Pan tostado cubierto con tomate fresco, ajo y albahaca", Prize= "6.50 / 8 / 14" },
        //            new DishResponse { Name="Calamares Crujientes", Description="Calamares fritos ligeramente, servidos con alioli de limón", Prize= "8.90" },
        //            new DishResponse { Name="Mejillones al Vapor", Description="Mejillones frescos con salsa de vino blanco y ajo", Prize= "7.50" },
        //            new DishResponse { Name="Ensalada Caprese", Description="Tomate, mozzarella y albahaca con reducción de balsámico", Prize= "6.00" },
        //            new DishResponse { Name="Croquetas Caseras", Description="Croquetas de jamón ibérico hechas a diario", Prize= "5.50" }
        //        ]
        //    },
        //    new MenuSectionResponse
        //    {
        //        Name = "Platos Principales",
        //        Note = "Todos los platos principales se sirven con una guarnición de verduras asadas.",
        //        Dishes =
        //        [
        //            new DishResponse { Name="Salmón a la Parrilla", Description="Filete de salmón fresco a la parrilla con mantequilla de hierbas", Prize= "18.50" },
        //            new DishResponse { Name="Lasaña Vegetariana", Description="Capas de pasta con verduras de temporada y bechamel cremosa", Prize= "15.00" },
        //            new DishResponse { Name="Entrecot de Ternera", Description="Jugoso entrecot de ternera cocinado al punto deseado", Prize= "22.00" },
        //            new DishResponse { Name="Pollo al Curry", Description="Pollo tierno cocinado en salsa de curry suave con arroz basmati", Prize= "16.50" },
        //            new DishResponse { Name="Paella de Mariscos", Description="Arroz, mariscos frescos y un toque de azafrán", Prize= "19.00" },
        //            new DishResponse { Name="Ratatouille", Description="Estofado de verduras mediterráneas con hierbas provenzales", Prize= "14.50" }
        //        ]
        //    },
        //    new MenuSectionResponse
        //    {
        //        Name = "Postres",
        //        Note = "Postres caseros elaborados a diario.",
        //        Dishes =
        //        [
        //            new DishResponse { Name="Coulant de Chocolate", Description="Bizcocho de chocolate caliente con corazón fundido", Prize="6.00" },
        //            new DishResponse { Name="Tarta de Queso", Description="Clásica tarta de queso cremosa con salsa de frutos rojos", Prize="5.50" },
        //            new DishResponse { Name="Helado Artesanal", Description="Varios sabores según disponibilidad", Prize="4.50" },
        //            new DishResponse { Name="Brownie con Nata", Description="Brownie de chocolate servido con nata montada", Prize="5.00" },
        //            new DishResponse { Name="Fruta de Temporada", Description="Selección fresca de fruta natural", Prize="3.50" }
        //        ]
        //    },
        //    new MenuSectionResponse
        //    {
        //        Name = "Especialidades de la Casa",
        //        Note = "Recomendado por nuestro chef.",
        //        Dishes =
        //        [
        //            new DishResponse { Name="Bacalao Confitado", Description="Bacalao cocinado lentamente con aceite de oliva y hierbas", Prize="20.00" },
        //            new DishResponse { Name="Rabo de Toro Estofado", Description="Rabo de toro cocinado a baja temperatura con verduras", Prize="21.50" },
        //            new DishResponse { Name="Carrilleras de Cerdo", Description="Carrilleras tiernas en salsa de vino tinto", Prize="19.50" },
        //            new DishResponse { Name="Risotto de Setas", Description="Arroz cremoso con setas de temporada y queso parmesano", Prize="17.00" }
        //        ]
        //    },
        //    new MenuSectionResponse
        //    {
        //        Name = "Bebidas",
        //        Note = "Pregunta a nuestro personal por la carta de vinos.",
        //        Dishes =
        //        [
        //            new DishResponse { Name="Agua Mineral", Description="Agua con gas o sin gas", Prize= "2.50" },
        //            new DishResponse { Name="Vino Tinto de la Casa", Description="Copa de nuestro vino tinto seleccionado", Prize= "4.00" },
        //            new DishResponse { Name="Cerveza Artesanal", Description="Cerveza local servida fría", Prize= "3.50" },
        //            new DishResponse { Name="Refresco", Description="Coca-Cola, Fanta o Sprite", Prize= "2.00" },
        //            new DishResponse { Name="Zumo Natural", Description="Naranja, manzana o zanahoria", Prize= "3.00" }
        //        ]
        //    },
        //]
        //    }
        //};



    }
}