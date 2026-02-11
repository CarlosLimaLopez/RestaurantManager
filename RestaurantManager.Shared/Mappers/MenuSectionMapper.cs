namespace RestaurantManager.MenuSections
{
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
    }
}
