using System.ComponentModel.DataAnnotations;

namespace RestaurantManager.Restaurant
{
    public class Restaurant
    {
        private Restaurant() { }

        public Restaurant(string name, string path, Menu menu, string? nameColor, string? description, string? logoPath)
        {
            Id = Guid.NewGuid();
            Name = name;
            Path = path;
            Menu = menu;
            NameColor = nameColor;
            Description = description;
            LogoPath = logoPath;
        }

        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public Menu Menu { get; set; }
        public string? NameColor { get; set; }
        public string? Description { get; set; }
        public string? LogoPath { get; set; }
    }
}
