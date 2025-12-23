using System.ComponentModel.DataAnnotations;

namespace RestaurantManager.Restaurant
{
    public class MenuSection
    {
        private MenuSection() { }

        public MenuSection(string name, IEnumerable<Dish> dishes, string? note)
        {
            Id = Guid.NewGuid();
            Name = name;
            Dishes = dishes;
            Note = note;
        }

        [Key]
        public Guid Id { get; init; }
        public string Name { get; set; }
        public IEnumerable<Dish> Dishes { get; set; }
        public string? Note { get; set; }
    }
}
