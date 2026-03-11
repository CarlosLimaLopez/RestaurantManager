namespace RestaurantManager.Menus
{
    using MenuSections;

    public static class MenuMapper
    {
        public static MenuUpdateRequest ToMenuUpdateRequest(this MenuGetResponse menu)
        {
            return new MenuUpdateRequest
            {
                NameSectionColor = menu.NameSectionColor,
                Note = menu.Note
            };
        }

        public static MenuGetResponse ToResponse(this Menu menu)
        {
            return new MenuGetResponse
            {
                Sections = menu.Sections?.Select(s => s.ToResponse()).ToArray() ?? [],
                Note = menu.Note,
                NameSectionColor = menu.NameSectionColor
            };
        }
    }
}
