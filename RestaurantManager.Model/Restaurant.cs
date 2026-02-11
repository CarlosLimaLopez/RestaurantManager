using System.ComponentModel.DataAnnotations;

namespace RestaurantManager.Restaurants
{
    using Menus;

    public class Restaurant
    {
#pragma warning disable CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de agregar el modificador "required" o declararlo como un valor que acepta valores NULL.
        public Restaurant() { }
#pragma warning restore CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de agregar el modificador "required" o declararlo como un valor que acepta valores NULL.

        public Restaurant(string name, string path, string? nameColor, string? description, string? descriptionColor, string? logoPath)
        {
            Name = name;
            Path = path;
            NameColor = nameColor;
            Description = description;
            LogoPath = logoPath;
            DescriptionColor = descriptionColor;
        }

        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public string Path { get; set; } = string.Empty;
        public List<Menu> Menus { get; init; } = [];
        public Menu? ActiveMenu { 
            get 
            { 
                return GetActiveMenu(DateOnly.FromDateTime(DateTime.Today)); 
            } 
        }
        public string? NameColor { get; set; } = null;
        public string? Description { get; set; } = null;
        public string? DescriptionColor { get; set; } = null;
        public string? LogoPath { get; set; } = null;

        public void AddMenu(Menu menu) => Menus.Add(menu);
        
        public Menu? GetActiveMenu(DateOnly date)
            => Menus
                .Where(m => m.ActivateAt <= date)
                .MaxBy(m => m.ActivateAt);
    }
}
