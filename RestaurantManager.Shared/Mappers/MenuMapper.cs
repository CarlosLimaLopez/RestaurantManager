namespace RestaurantManager.Menus
{
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
    }
}
