namespace RestaurantManager.Interface.DTOs.Response
{
    public class RestaurantResponse
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public string NameColor { get; set; } = string.Empty;
        public string? Description { get; set; } = null;
        public string? LogoPath { get; set; } = null;
        public MenuResponse Menu { get; set; } = new MenuResponse();
    }

    public class MenuResponse
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Note { get; set; } = string.Empty;
        public List<MenuSectionResponse> Sections { get; set; } = [];
    }

    public class MenuSectionResponse
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public string NameColor { get; set; } = string.Empty;
        public List<DishResponse> Dishes { get; set; } = [];
        public string Note { get; set; } = string.Empty;

    }

    public class DishResponse
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Prize { get; set; } = string.Empty;
    }
}
