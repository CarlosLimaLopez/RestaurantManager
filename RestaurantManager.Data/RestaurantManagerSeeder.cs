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

            var casaUmami = CasaUmamiData();
            var laMaura = LaMauraData();

            context.Restaurants.Add(casaUmami);
            context.Restaurants.Add(laMaura);

            context.SaveChanges();
        }

        public static Restaurant CasaUmamiData()
        {
            var entrantes = new MenuSection(
                "Entrantes",
                1,
                [
                    new Dish("Bruschetta", "6.50", "Pan tostado cubierto con tomate fresco, ajo y albahaca"),
                    new Dish("Calamares Crujientes", "8.90", "Calamares fritos ligeramente, servidos con alioli de limón")
                ],
                "Perfectos para compartir antes del plato principal."
            );

            var platosPrincipales = new MenuSection(
                "Platos Principales",
                2,
                [
                    new Dish("Salmón a la Parrilla", "18.50", "Filete de salmón fresco a la parrilla con mantequilla de hierbas"),
                    new Dish("Entrecot de Ternera", "22.00", "Jugoso entrecot de ternera cocinado al punto deseado"),
                    new Dish("Lasaña Vegetariana", "15.00", "Capas de pasta con verduras de temporada y bechamel cremosa")
                ],
                "Todos los platos principales se sirven con una guarnición de verduras asadas."
            );

            var postres = new MenuSection(
                "Postres",
                3,
                [
                    new Dish("Coulant de Chocolate", "6.00", "Bizcocho de chocolate caliente con corazón fundido"),
                    new Dish("Tarta de Queso", "5.50", "Clásica tarta de queso cremosa con salsa de frutos rojos")
                ],
                "Postres caseros elaborados a diario."
            );

            var bebidas = new MenuSection(
                "Bebidas",
                4,
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
                DateOnly.MinValue ,
                "Los precios incluyen IVA. Por favor, informa al personal de cualquier alergia.",
                "#59151D"
            );

            var restaurant = new Restaurant(
                "Casa Umami", 
                "casa-umami", 
                "#59151D",
                "Bienvenidos a Casa Umami, donde cada plato es una experiencia culinaria única.",
                descriptionColor: null,
                "images/logos/casa-umami-logo.png");

            restaurant.AddMenu(menu);

            return restaurant;
        }

        public static Restaurant LaMauraData()
        {
            var menuSections = new List<MenuSection>
            {
                new MenuSection(
                    "Aperitivos",
                    1,
                    [
                        new("Aceitunas gordales machacadas", "3"),
                        new("Gildas", "3.5", "Atún de Almadraba, queso mozzarella, tomate cherry y piparra"),
                        new("Jamón ibérico", "12 / 24", "100% raza ibérica de bellota \"Lazo\""),
                        new("Queso Antaño", "7 / 12", "Elaborado con leche cruda de cabra con 12-14 meses de maduración"),
                        new("Tabla de quesos", "16", "Queso Antaño, trufado, payoyo y roquefort"),
                        new("Mojama del Rey de Oros", "4 / 8"),
                        new("Anchoas 0'0", "18", "6 anchoas del cantábrico acompañadas de queso roquefort, salmorejo y tostas de croissant")
                    ],
                    null
                ),
                new MenuSection(
                    "Entrantes",
                    2,
                    [
                        new("Ensaladilla de gambas al ajillo", "6 / 10", "Chips de ajo, perejil frito y salsa cóctel"),
                        new("Ensaladilla de Almadraba", "6 / 10", "Con daditos de atún picante, mayo kimchi y cebollino"),
                        new("Ensalada de tomate rosa", "7 / 14", "Cama de mézclum, flor de aguacate, tronco de melva, cebolla morada, piparras y vinagreta de cítricos"),
                        new("Nuestra versión del salpicón de gambón y pulpo", "11", "Tomate rosa osmotizado con vinagre de jerez, cebolleta confitada, gambón y pulpo cocido con emulsión de pimientos amarillos asados"),
                        new("Vieira", "3.5 ud", "Con mayonesa de kimchi y lima gratinada"),
                        new("Carpaccio de salmón", "15", "Glaseado con daditos de aguacate, mango, tomate cherry confitado y mozzarella rallada"),
                        new("Croquetas de puchero", "7 / 14", "Sobre alioli de perejil y velo de jamón"),
                        new("Tataki de presa ibérica", "15", "Macerado durante 4 horas acompañado de salsa de pimiento rojo asado"),
                        new("Canelón de carrillá", "11", "Con bechamel trufada y parmesano gratinado"),
                        new("Tosta de atún", "7.5", "En pan de cristal con nuestra mayo de trufa"),
                        new("Ensalada cítrica", "24", "Ensalada de mango, aguacate, cebolla morada, tomate, piparras, pulpo y atún"),
                        new("Tartar de descargamento", "19", "Láminas de aguacate con toque de mayonesa trufada"),
                        new("Tartar con burrata", "19", "Burrata agripicante con atún macerado y ralladura de limón"),
                        new("Tartar de ventresca", "20", "Con yema de huevo curada en soja"),
                        new("Sashimi de ventresca", "16"),
                        new("Pan brioche", "7.5 ud", "Con crema de queso camembert y atún macerado"),
                        new("Canelón de calabacín", "17", "Atún picante con cebolla morada envuelto en láminas de calabacín asado y hueva de tobiko"),
                        new("Ajo blanco", "19", "Tartar de descargamento con sopa fría de almendra"),
                        new("Tataki nori", "20", "Lingote de atún macerado envuelto en alga nori frito en tempura acompañado de pico de gallo"),
                        new("Huevos rotos", "18", "Daditos de atún picante con huevos fritos y puntillitas"),
                        new("Revuelto salvaje", "18", "Patata laminada pochada con salsa de trufa, atún macerado y huevo poché"),
                        new("Ravioli", "16", "Atún encebollado envuelto en pasta wonton sobre parmentier napado con jugo de atún"),
                        new("Wok de atún", "17", "Noodles salteados con verdura, setas shiitake, atún, teriyaki y almendras"),
                        new("Nuestro atún con tomate", "20", "Solomillo de atún sobre pisto de tomate confitado"),
                        new("Tarantelo mechado", "22", "Glaseado en su propio jugo sobre una cama de patatas chips"),
                        new("Surtido de crudos", "36", "Tartar de ventresca, Tartar de descargamento picante, Sashimi de ventresca, Tataki de descargamento")
                    ],
                    null
                ),
                new MenuSection(
                    "Principales",
                    3,
                    [
                        new("Parpatana de atún", "26", "Con guarnición a elegir"),
                        new("Ventresca de atún", "28", "Con guarnición a elegir"),
                        new("Solomillo de atún", "22", "Con guarnición a elegir"),
                        new("Pata de pulpo", "24", "Sobre cremoso de patatas"),
                        new("Lomo de rodaballo al pil pil", "6.5 / 100g"),
                        new("Lenguado al pil pil", "7 / 100g"),
                        new("Jarrete de cordero a baja temperatura", "26", "Glaseado en su propio jugo acompañado de patatas arrugadas con mantequilla de ajo-perejil"),
                        new("Solomillo de ternera trufado", "22", "Sobre parmentier napado con jugo trufado")
                    ],
                    null
                ),
                new MenuSection(
                    "Brasas",
                    4,
                    [
                        new("Lingote de presa ibérica", "21"),
                        new("Solomillo de ternera", "9 / 100g"),
                        new("Entrecot de ternera", "45 / pieza"),
                        new("Picaña de rubia gallega", "7.5 / 100g"),
                        new("Lomo bajo de blanck angus uruguayo", "7.5 / 100g"),
                        new("Wagyu Finca Santa Rosalía (Chuletón)", "11.5 / 100g"),
                        new("Rubia gallega (Chuletón)", "9.5 / 100g"),
                        new("Black angus (Chuletón)", "9 / 100g"),
                        new("Simmental (Chuletón)", "7 / 100g"),
                        new("T-Bone Frisona", "7.5 / 100g")
                    ],
                    "Todas nuestras carnes a la brasa van acompañadas de patatas fritas y pimientos del padrón"
                ),
                new MenuSection(
                    "Para los más peques",
                    5,
                    [
                        new("Filete de pollo con patatas", "9"),
                        new("Crispy burger", "10"),
                        new("Patatas fritas con huevo", "6")
                    ],
                    null
                ),
                new MenuSection(
                    "Carta de vinos",
                    6,
                    [],
                    null
                )
            };

            var menu = new Menu(
                menuSections,
                DateOnly.MinValue,
                note: "Todos los precios están en euros e incluyen un 10% de IVA.",
                nameSectionColor: "#018D84"
            );

            var restaurant = new Restaurant(
                name: "La Maura",
                path: "la-maura",
                nameColor: "#018D84",
                description: "NEOTABERNA",
                descriptionColor: null,
                logoPath: "images/logos/la-maura-logo.webp");

            restaurant.AddMenu(menu);

            return restaurant;
        }
    }
}
