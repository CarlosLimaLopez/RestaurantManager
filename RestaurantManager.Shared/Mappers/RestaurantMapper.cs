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
                        Allergens = d.Allergens.Select(a => a.ToResponse()).ToList()
                    }).OrderBy(d => d.Order).ToList()
                }).OrderBy(s => s.Order).ToArray()
            }).ToArray()
        };
        
        public static RestaurantUpdateRequest ToRestaurantUpdateRequest(this RestaurantGetResponse restaurant)
        => new()
        {
            Name = restaurant.Name,
            NameColor = restaurant.NameColor,
            Path = restaurant.Path,
            Description = restaurant.Description,
            DescriptionColor = restaurant.DescriptionColor,
            LogoPath = restaurant.LogoPath
        };
    }
}
