namespace RestaurantManager.MenuSections
{
    public class MenuSectionUpdateRequest
    {
        public Guid MenuSectionId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Order { get; set; }
        public string? NameSectionColor { get; set; } = string.Empty;
        public string? Note { get; set; } = null;
    }
}
