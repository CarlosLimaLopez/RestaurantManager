using System.ComponentModel.DataAnnotations;

namespace RestaurantManager.MenuSections
{
    using Dishes;

    public class MenuSection
    {
        public MenuSection() { }

        public MenuSection(string name, int order, List<Dish> dishes, string? note)
        {
            Order = order;
            Name = name;
            Dishes = dishes;
            Note = note;
        }

        [Key]
        public Guid Id { get; init; }
        public string Name { get; set; } = string.Empty;
        public int Order { get; set; }
        public List<Dish> Dishes { get; set; } = [];
        public string? Note { get; set; } = null;
    }
}
