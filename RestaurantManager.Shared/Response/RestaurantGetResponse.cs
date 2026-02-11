namespace RestaurantManager.Restaurants
{
    using Menus;

    public class RestaurantGetResponse
    {
        public Guid Id { get; set; } = Guid.Empty;
        public string Name { get; set; } = string.Empty;
        public string? NameColor { get; set; } = null;
        public string Path { get; set; } = string.Empty;
        public string? Description { get; set; } = null;
        public string? DescriptionColor { get; set; } = null;
        public string? LogoPath { get; set; } = null;
        public MenuGetResponse[] Menus { get; set; } = [];
    }
}
