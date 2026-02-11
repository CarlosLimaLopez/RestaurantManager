using System.ComponentModel.DataAnnotations;

namespace RestaurantManager.Dishes
{
    using RestaurantManager.Allergens;

    public class Dish
    {
#pragma warning disable CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de agregar el modificador "required" o declararlo como un valor que acepta valores NULL.
        public Dish() { }
#pragma warning restore CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de agregar el modificador "required" o declararlo como un valor que acepta valores NULL.

        public Dish(string name, int order, IEnumerable<AllergenType> allergens, string? prize, string? description = null)
        {
            Name = name;
            Order = order;
            Allergens = allergens;
            Prize = prize;
            Description = description;
        }

        [Key]
        public Guid Id { get; init; }
        public string Name { get; set; } = string.Empty;
        public int Order { get; set; }
        public IEnumerable<AllergenType> Allergens { get; set; } = [];
        public string? Description { get; set; } = string.Empty;
        public string? Prize { get; set; } = string.Empty;
    }
}
