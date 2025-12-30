namespace RestaurantManager.Restaurant
{
    public class RestaurantResponse
    {
        public string Name { get; set; } = string.Empty;
        public string? NameColor { get; set; } = null;
        public string Path { get; set; } = string.Empty;
        public MenuResponse ActiveMenu { get; set; } = new MenuResponse();
        public string? Description { get; set; } = null;
        public string? DescriptionColor { get; set; } = null;
        public string? LogoPath { get; set; } = null;
        public List<AllergenResponse> Allergens { get; set; } = [];
    }

    public class MenuResponse
    {
        public List<MenuSectionResponse> Sections { get; set; } = [];
        public string? Note { get; set; } = null;
        public string? NameSectionColor { get; set; } = null;
    }

    public class MenuSectionResponse
    {
        public string Name { get; set; } = string.Empty;
        public int Order { get; set; }
        public string? NameSectionColor { get; set; } = string.Empty;
        public List<DishResponse> Dishes { get; set; } = [];
        public string? Note { get; set; } = null;

    }

    public class DishResponse
    {
        public string Name { get; set; } = string.Empty;
        public int Order { get; set; }
        public IEnumerable<AllergenResponse> Allergens { get; set; } = [];
        public string? Description { get; set; } = null;
        public string? Prize { get; set; } = null;
    }

    public class AllergenResponse
    {
        public string Type { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Icon { get; set; } = string.Empty;
    }

    public static class RestaurantMapper
    {
        public static RestaurantResponse ToResponse(this Restaurant restaurant)
        {
            var allergens = restaurant.ActiveMenu == null ? [] : restaurant.ActiveMenu.Sections
                .SelectMany(s => s.Dishes)
                .SelectMany(d => d.Allergens)
                .Distinct()
                .Select(a => a.ToResponse())
                .ToList();

            return new RestaurantResponse
            {
                Name = restaurant.Name,
                NameColor = restaurant.NameColor,
                Path = restaurant.Path,
                Description = restaurant.Description,
                DescriptionColor = restaurant.DescriptionColor,
                LogoPath = restaurant.LogoPath,
                ActiveMenu = restaurant.ActiveMenu != null
                    ? restaurant.ActiveMenu.ToResponse()
                    : new MenuResponse(),
                Allergens = allergens
            };
        }

        public static MenuResponse ToResponse(this Menu menu)
        {
            return new MenuResponse
            {
                Sections = menu.Sections?.Select(s => s.ToResponse()).ToList() ?? [],
                Note = menu.Note,
                NameSectionColor = menu.NameSectionColor
            };
        }

        public static MenuSectionResponse ToResponse(this MenuSection section)
        {
            return new MenuSectionResponse
            {
                Name = section.Name,
                Order = section.Order,
                Note = section.Note,
                Dishes = section.Dishes?.Select(d => d.ToResponse()).ToList() ?? []
            };
        }

        public static DishResponse ToResponse(this Dish dish)
        {
            return new DishResponse
            {
                Name = dish.Name,
                Order = dish.Order,
                Description = dish.Description,
                Prize = dish.Prize,
                Allergens = dish.Allergens?.Select(a => a.ToResponse()).ToList() ?? []
            };
        }

        public static AllergenResponse ToResponse(this AllergenType allergenType)
        {
            var allergen = Allergens.GetByEnum(allergenType);

            if(allergen == null)
            {
                return new AllergenResponse
                {
                    Type = allergenType.ToString(),
                    Name = string.Empty,
                    Icon = string.Empty
                };
            }

            return new AllergenResponse
            {
                Type = allergen.Type.ToString(),
                Name = allergen.Name,
                Icon = allergen.Icon
            };
        }
    }
}
