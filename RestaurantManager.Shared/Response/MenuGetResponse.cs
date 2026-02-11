namespace RestaurantManager.Menus
{
    using MenuSections;

    public class MenuGetResponse
    {
        public Guid MenuId { get; set; }
        public DateOnly ActivateAt { get; set; }
        public string? Note { get; set; } = null;
        public string? NameSectionColor { get; set; } = null;
        public MenuSectionGetResponse[] Sections { get; set; } = [];
    }
}
