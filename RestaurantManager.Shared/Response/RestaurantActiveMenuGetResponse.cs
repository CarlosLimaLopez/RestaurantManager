namespace RestaurantManager.Restaurants
{
    using Menus;
    using Allergens;

    public class RestaurantActiveMenuGetResponse
    {
        public string Name { get; set; } = string.Empty;
        public string? NameColor { get; set; } = null;
        public string Path { get; set; } = string.Empty;
        public MenuGetResponse ActiveMenu { get; set; } = new MenuGetResponse();
        public string? Description { get; set; } = null;
        public string? DescriptionColor { get; set; } = null;
        public string? LogoPath { get; set; } = null;
        public List<AllergenGetResponse> Allergens { get; set; } = [];
    }
}
