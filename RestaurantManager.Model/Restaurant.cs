using System.ComponentModel.DataAnnotations;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RestaurantManager.Restaurant
{
    public class Restaurant
    {
        private Restaurant() { }

        public Restaurant(string name, string path, string? nameColor, string? description, string? descriptionColor, string? logoPath)
        {
            Id = Guid.NewGuid();
            Name = name;
            Path = path;
            Menus = [];
            NameColor = nameColor;
            Description = description;
            LogoPath = logoPath;
            DescriptionColor = descriptionColor;
        }

        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public List<Menu> Menus { get; set; }
        public Menu? ActiveMenu { 
            get 
            { 
                return GetActiveMenu(DateOnly.FromDateTime(DateTime.Today)); 
            } 
        }
        public string? NameColor { get; set; }
        public string? Description { get; set; }
        public string? DescriptionColor { get; set; }
        public string? LogoPath { get; set; }

        public void AddMenu(Menu menu) => Menus.Add(menu);
        
        public Menu? GetActiveMenu(DateOnly date)
            => Menus
                .Where(m => m.ActivateAt <= date)
                .MaxBy(m => m.ActivateAt);
    }
}
