namespace RestaurantManager.MenuSections
{
    using Dishes;

    public class MenuSectionGetResponse
    {
        public Guid MenuSectionId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Order { get; set; }
        public string? NameSectionColor { get; set; } = string.Empty;
        public List<DishGetResponse> Dishes { get; set; } = [];
        public string? Note { get; set; } = null;
    }
}
