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
                    new Dish("Bruschetta", 1, [], "6.50", "Pan tostado cubierto con tomate fresco, ajo y albahaca"),
                    new Dish("Calamares Crujientes", 2, [], "8.90", "Calamares fritos ligeramente, servidos con alioli de limón")
                ],
                "Perfectos para compartir antes del plato principal."
            );

            var platosPrincipales = new MenuSection(
                "Platos Principales",
                2,
                [
                    new Dish("Salmón a la Parrilla", 1, [], "18.50", "Filete de salmón fresco a la parrilla con mantequilla de hierbas"),
                    new Dish("Entrecot de Ternera", 2, [], "22.00", "Jugoso entrecot de ternera cocinado al punto deseado"),
                    new Dish("Lasaña Vegetariana", 3, [], "15.00", "Capas de pasta con verduras de temporada y bechamel cremosa")
                ],
                "Todos los platos principales se sirven con una guarnición de verduras asadas."
            );

            var postres = new MenuSection(
                "Postres",
                3,
                [
                    new Dish("Coulant de Chocolate", 1, [], "6.00", "Bizcocho de chocolate caliente con corazón fundido"),
                    new Dish("Tarta de Queso", 2, [], "5.50", "Clásica tarta de queso cremosa con salsa de frutos rojos")
                ],
                "Postres caseros elaborados a diario."
            );

            var bebidas = new MenuSection(
                "Bebidas",
                4,
                [
                    new Dish("Agua Mineral", 1, [], "2.50", "Agua con gas o sin gas"),
                    new Dish("Vino Tinto de la Casa", 2, [], "4.00", "Copa de nuestro vino tinto seleccionado")
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
                        new("Aceitunas gordales machacadas", 1, [], "3"),
                        new("Gildas", 2, [AllergenType.Soy, AllergenType.Lactose, AllergenType.Fish], "3.5", "Atún de Almadraba, queso mozzarella, tomate cherry y piparra"),
                        new("Jamón ibérico", 3, [], "12 / 24", "100% raza ibérica de bellota \"Lazo\""),
                        new("Queso Antaño", 4, [AllergenType.Lactose], "7 / 12", "Elaborado con leche cruda de cabra con 12-14 meses de maduración"),
                        new("Tabla de quesos", 5, [AllergenType.Soy, AllergenType.Lactose], "16", "Queso Antaño, trufado, payoyo y roquefort"),
                        new("Mojama del Rey de Oros", 6, [AllergenType.Soy], "4 / 8"),
                        new("Anchoas 0'0", 7, [AllergenType.Gluten, AllergenType.Lactose, AllergenType.Fish], "18", "6 anchoas del cantábrico acompañadas de queso roquefort, salmorejo y tostas de croissant")
                    ],
                    null
                ),
                new MenuSection(
                    "Entrantes",
                    2,
                    [
                        new("Ensaladilla de gambas al ajillo", 1, [AllergenType.Eggs, AllergenType.Mollusks, AllergenType.Crustaceans], "6 / 10", "Chips de ajo, perejil frito y salsa cóctel"),
                        new("Ensaladilla de Almadraba", 2, [AllergenType.Eggs, AllergenType.Fish], "6 / 10", "Con daditos de atún picante, mayo kimchi y cebollino"),
                        new("Ensalada de tomate rosa", 3, [AllergenType.Fish], "7 / 14", "Cama de mézclum, flor de aguacate, tronco de melva, cebolla morada, piparras y vinagreta de cítricos"),
                        new("Nuestra versión del salpicón de gambón y pulpo", 4, [AllergenType.Crustaceans], "11", "Tomate rosa osmotizado con vinagre de jerez, cebolleta confitada, gambón y pulpo cocido con emulsión de pimientos amarillos asados"),
                        new("Vieira", 5, [AllergenType.Eggs, AllergenType.Mollusks], "3.5 ud", "Con mayonesa de kimchi y lima gratinada"),
                        new("Carpaccio de salmón", 6, [AllergenType.Gluten, AllergenType.Sesame, AllergenType.Lactose, AllergenType.Fish], "15", "Glaseado con daditos de aguacate, mango, tomate cherry confitado y mozzarella rallada"),
                        new("Croquetas de puchero", 7, [AllergenType.Gluten, AllergenType.Celery, AllergenType.Eggs], "7 / 14", "Sobre alioli de perejil y velo de jamón"),
                        new("Tataki de presa ibérica", 8, [AllergenType.Sesame, AllergenType.TreeNuts], "15", "Macerado durante 4 horas acompañado de salsa de pimiento rojo asado"),
                        new("Canelón de carrillá", 9, [AllergenType.Mollusks, AllergenType.Crustaceans, AllergenType.Sesame, AllergenType.TreeNuts, AllergenType.Lactose, AllergenType.Gluten], "11", "Con bechamel trufada y parmesano gratinado"),
                    ],
                    null
                ),
                new MenuSection(
                    "Atún rojo salvaje de Almadraba",
                    3,
                    [
                        new("Tosta de atún", 1, [AllergenType.Gluten, AllergenType.Eggs, AllergenType.Fish, AllergenType.Crustaceans, AllergenType.Soy], "7.5", "En pan de cristal con nuestra mayo de trufa"),
                        new("Ensalada cítrica", 2, [AllergenType.Crustaceans, AllergenType.Fish, AllergenType.Mustard], "24", "Ensalada de mango, aguacate, cebolla morada, tomate, piparras, pulpo y atún"),
                        new("Tartar de descargamento", 3, [AllergenType.Crustaceans, AllergenType.Fish, AllergenType.Eggs], "19", "Láminas de aguacate con toque de mayonesa trufada"),
                        new("Tartar con burrata", 4, [AllergenType.Fish, AllergenType.Lactose], "19", "Burrata agripicante con atún macerado y ralladura de limón"),
                        new("Tartar de ventresca", 5, [AllergenType.Crustaceans, AllergenType.Fish, AllergenType.Eggs], "20", "Con yema de huevo curada en soja"),
                        new("Sashimi de ventresca", 6, [AllergenType.Fish], "16"),
                        new("Pan brioche", 7, [AllergenType.Crustaceans, AllergenType.Fish, AllergenType.Lactose], "7.5 ud", "Con crema de queso camembert y atún macerado"),
                        new("Canelón de calabacín", 8, [AllergenType.Fish, AllergenType.Sesame], "17", "Atún picante con cebolla morada envuelto en láminas de calabacín asado y hueva de tobiko"),
                        new("Ajo blanco", 9, [AllergenType.Gluten, AllergenType.Fish, AllergenType.TreeNuts], "19", "Tartar de descargamento con sopa fría de almendra"),
                        new("Tataki nori", 10, [AllergenType.Gluten, AllergenType.Fish, AllergenType.TreeNuts, AllergenType.Sesame], "20", "Lingote de atún macerado envuelto en alga nori frito en tempura acompañado de pico de gallo"),
                        new("Huevos rotos", 11, [AllergenType.Crustaceans, AllergenType.Fish, AllergenType.Eggs, AllergenType.Mollusks], "18", "Daditos de atún picante con huevos fritos y puntillitas"),
                        new("Revuelto salvaje", 12, [AllergenType.Crustaceans, AllergenType.Fish, AllergenType.Eggs, AllergenType.Mollusks], "18", "Patata laminada pochada con salsa de trufa, atún macerado y huevo poché"),
                        new("Ravioli", 13, [AllergenType.Fish, AllergenType.Eggs, AllergenType.Gluten, AllergenType.Sulfites], "16", "Atún encebollado envuelto en pasta wonton sobre parmentier napado con jugo de atún"),
                        new("Wok de atún", 14, [AllergenType.Gluten, AllergenType.TreeNuts, AllergenType.Soy, AllergenType.Fish, AllergenType.Eggs], "17", "Noodles salteados con verdura, setas shiitake, atún, teriyaki y almendras"),
                        new("Nuestro atún con tomate", 15, [AllergenType.Fish], "20", "Solomillo de atún sobre pisto de tomate confitado"),
                        new("Tarantelo mechado", 16, [AllergenType.Fish, AllergenType.Lactose, AllergenType.Sulfites], "22", "Glaseado en su propio jugo sobre una cama de patatas chips"),
                        new("Surtido de crudos", 17, [AllergenType.Fish], "36", "Tartar de ventresca, Tartar de descargamento picante, Sashimi de ventresca, Tataki de descargamento")
                    ],
                    null
                ),
                new MenuSection(
                    "Principales",
                    4,
                    [
                        new("Parpatana de atún", 1, [AllergenType.Fish], "26", "Con guarnición a elegir"),
                        new("Ventresca de atún", 2, [AllergenType.Fish], "28", "Con guarnición a elegir"),
                        new("Solomillo de atún", 3, [AllergenType.Fish], "22", "Con guarnición a elegir"),
                        new("Pata de pulpo", 4, [AllergenType.Sulfites], "24", "Sobre cremoso de patatas"),
                        new("Lomo de rodaballo al pil pil", 5, [AllergenType.Fish, AllergenType.Sulfites], "6.5 / 100g"),
                        new("Lenguado al pil pil", 6, [AllergenType.Fish, AllergenType.Sulfites], "7 / 100g"),
                        new("Jarrete de cordero a baja temperatura", 7, [AllergenType.Lactose], "26", "Glaseado en su propio jugo acompañado de patatas arrugadas con mantequilla de ajo-perejil"),
                        new("Solomillo de ternera trufado", 8, [AllergenType.Lactose], "22", "Sobre parmentier napado con jugo trufado")
                    ],
                    null
                ),
                new MenuSection(
                    "Brasas",
                    5,
                    [
                        new("Lingote de presa ibérica", 1, [], "21"),
                        new("Solomillo de ternera", 2, [], "9 / 100g"),
                        new("Entrecot de ternera", 3, [], "45 / pieza"),
                        new("Picaña de rubia gallega", 4, [], "7.5 / 100g"),
                        new("Lomo bajo de blanck angus uruguayo", 5, [], "7.5 / 100g"),
                        new("Wagyu Finca Santa Rosalía", 6, [], "11.5 / 100g", "(Chuletón)"),
                        new("Rubia gallega", 7, [], "9.5 / 100g", "(Chuletón)"),
                        new("Black angus", 8, [], "9 / 100g", "(Chuletón)"),
                        new("Simmental", 9, [], "7 / 100g", "(Chuletón)"),
                        new("T-Bone Frisona", 10, [], "7.5 / 100g")
                    ],
                    "Todas nuestras carnes a la brasa van acompañadas de patatas fritas y pimientos del padrón"
                ),
                new MenuSection(
                    "Para los más peques",
                    6,
                    [
                        new("Filete de pollo con patatas", 1, [], "9"),
                        new("Crispy burger", 2, [AllergenType.Eggs, AllergenType.Crustaceans, AllergenType.Lactose], "10"),
                        new("Patatas fritas con huevo", 3, [AllergenType.Eggs], "6")
                    ],
                    null
                ),
                new MenuSection(
                    "Carta de vinos",
                    7,
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
                logoPath: "images/logos/la-maura-logo-1a.webp");

            restaurant.AddMenu(menu);

            return restaurant;
        }
    }
}
