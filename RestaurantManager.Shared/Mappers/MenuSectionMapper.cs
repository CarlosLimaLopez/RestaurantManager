namespace RestaurantManager.MenuSections
{
    using Dishes;

    public static class MenuSectionMapper
    {
        public static MenuSectionUpdateRequest ToMenuSectionUpdateRequest(this MenuSectionGetResponse menu)
        {
            return new MenuSectionUpdateRequest
            {
                MenuSectionId = menu.MenuSectionId,
                Name = menu.Name,
                Order = menu.Order,
                NameSectionColor = menu.NameSectionColor,
                Note = menu.Note
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
    }
}
