namespace RestaurantManager.Restaurants
{
    using Dishes;
    using Menus;
    using MenuSections;
    using Allergens;

    public static partial class RestaurantMapper
    {
        public static RestaurantGetResponse ToRestaurantGetResponse(this Restaurant restaurant)
        => new() { 
            Id = restaurant.Id,
            Name = restaurant.Name,
            NameColor = restaurant.NameColor,
            Path = restaurant.Path,
            Description = restaurant.Description,
            DescriptionColor = restaurant.DescriptionColor,
            LogoPath = restaurant.LogoPath,
            Menus = restaurant.Menus.Select(m => new MenuGetResponse()
            {
                MenuId = m.Id,
                ActivateAt = m.ActivateAt,
                Note = m.Note,
                NameSectionColor = m.NameSectionColor,
                Sections = m.Sections.Select(s => new MenuSectionGetResponse()
                {
                    MenuSectionId = s.Id,
                    Name = s.Name,
                    Order = s.Order,
                    Note = s.Note,
                    Dishes = s.Dishes.Select(d => new DishGetResponse()
                    {
                        DishId = d.Id,
                        Name = d.Name,
                        Description = d.Description,
                        Prize = d.Prize,
                        Order = d.Order,
                        Allergens = d.Allergens.Select(a => a.ToResponse()).ToList(),
                        HasImage = d.HasImage
                    }).OrderBy(d => d.Order).ToList()
                }).OrderBy(s => s.Order).ToArray()
            }).ToArray()
        };

        public static RestaurantUpdateRequest ToRestaurantUpdateRequest(this RestaurantSummaryGetResponse restaurant)
        {
            return new RestaurantUpdateRequest
            {
                Name = restaurant.Name,
                NameColor = restaurant.NameColor,
                Path = restaurant.Path,
                Description = restaurant.Description,
                DescriptionColor = restaurant.DescriptionColor,
                LogoPath = restaurant.LogoPath,
                Public = restaurant.Public
            };
        }

        public static RestaurantActiveMenuGetResponse ToRestaurantActiveMenuGetResponse(this Restaurant restaurant)
        {
            var allergens = restaurant.ActiveMenu == null ? [] : restaurant.ActiveMenu.Sections
                .SelectMany(s => s.Dishes)
                .SelectMany(d => d.Allergens)
                .Distinct()
                .Select(a => a.ToResponse())
                .ToList();

            return new RestaurantActiveMenuGetResponse
            {
                Name = restaurant.Name,
                NameColor = restaurant.NameColor,
                Path = restaurant.Path,
                Description = restaurant.Description,
                DescriptionColor = restaurant.DescriptionColor,
                LogoPath = restaurant.LogoPath,
                ActiveMenu = restaurant.ActiveMenu != null
                    ? restaurant.ActiveMenu.ToResponse()
                    : new MenuGetResponse(),
                Allergens = allergens
            };
        }

        public static RestaurantSummaryGetResponse ToRestaurantSummaryGetResponse(this Restaurant restaurant)
        {
            return new RestaurantSummaryGetResponse
            {
                Id = restaurant.Id,
                Name = restaurant.Name,
                NameColor = restaurant.NameColor,
                Path = restaurant.Path,
                Description = restaurant.Description,
                DescriptionColor = restaurant.DescriptionColor,
                LogoPath = restaurant.LogoPath,
                Public = restaurant.Public
            };
        }
    }
}
