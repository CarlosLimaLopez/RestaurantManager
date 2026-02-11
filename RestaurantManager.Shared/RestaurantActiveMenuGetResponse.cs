namespace RestaurantManager.Restaurants
{
    using Menus;
    using MenuSections;
    using Dishes;
    using Allergens;

    public static partial class RestaurantMapper
    {
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
                LogoPath = restaurant.LogoPath
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

        #region Update
        public static RestaurantUpdateRequest ToRestaurantUpdateRequest(this RestaurantSummaryGetResponse restaurant)
        {
            return new RestaurantUpdateRequest
            {
                Name = restaurant.Name,
                NameColor = restaurant.NameColor,
                Path = restaurant.Path,
                Description = restaurant.Description,
                DescriptionColor = restaurant.DescriptionColor,
                LogoPath = restaurant.LogoPath
            };
        }

        
        #endregion

        public static MenuGetResponse ToResponse(this Menu menu)
        {
            return new MenuGetResponse
            {
                Sections = menu.Sections?.Select(s => s.ToResponse()).ToArray() ?? [],
                Note = menu.Note,
                NameSectionColor = menu.NameSectionColor
            };
        }

        public static MenuSectionGetResponse ToResponse(this MenuSection section)
        {
            return new MenuSectionGetResponse
            {
                Name = section.Name,
                Order = section.Order,
                Note = section.Note,
                Dishes = section.Dishes?.Select(d => d.ToResponse()).ToList() ?? []
            };
        }

        public static DishGetResponse ToResponse(this Dish dish)
        {
            return new DishGetResponse
            {
                Name = dish.Name,
                Order = dish.Order,
                Description = dish.Description,
                Prize = dish.Prize,
                Allergens = dish.Allergens?.Select(a => a.ToResponse()).ToList() ?? []
            };
        }
    }
}
