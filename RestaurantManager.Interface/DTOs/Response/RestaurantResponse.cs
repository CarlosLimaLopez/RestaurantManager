namespace RestaurantManager.Interface.DTOs.Response
{
    public class RestaurantResponse
    {
        public string Name { get; set; } = string.Empty;
        public string? NameColor { get; set; } = null;
        public string Path { get; set; } = string.Empty;
        public MenuResponse ActiveMenu { get; set; } = new MenuResponse();
        public string? Description { get; set; } = null;
        public string? DescriptionColor { get; set; } = null;
        public string? LogoPath { get; set; } = null;
    }

    public class MenuResponse
    {
        public List<MenuSectionResponse> Sections { get; set; } = [];
        public string? Note { get; set; } = null;
        public string? NameSectionColor { get; set; } = null;
    }

    public class MenuSectionResponse
    {
        public string Name { get; set; } = string.Empty;
        public int Order { get; set; }
        public string? NameSectionColor { get; set; } = string.Empty;
        public List<DishResponse> Dishes { get; set; } = [];
        public string? Note { get; set; } = null;

    }

    public class DishResponse
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; } = null;
        public string? Prize { get; set; } = null;
    }
}
