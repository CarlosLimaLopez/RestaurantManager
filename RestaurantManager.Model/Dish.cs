using System.ComponentModel.DataAnnotations;

namespace RestaurantManager.Restaurant
{
    public class Dish
    {
        private Dish() { }

        public Dish(string name, int order, IEnumerable<AllergenType> allergens, string? prize, string? description = null)
        {
            Id = Guid.NewGuid();
            Name = name;
            Order = order;
            Allergens = allergens;
            Prize = prize;
            Description = description;
        }

        [Key]
        public Guid Id { get; init; }
        public string Name { get; set; }
        public int Order { get; set; }
        public IEnumerable<AllergenType> Allergens { get; set; }
        public string? Description { get; set; }    
        public string? Prize { get; set; }
    }
}
