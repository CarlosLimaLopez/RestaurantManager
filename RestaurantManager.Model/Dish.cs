using System.ComponentModel.DataAnnotations;

namespace RestaurantManager.Restaurant
{
    public class Dish
    {
        private Dish() { }

        public Dish(string name, string? prize, string? description = null)
        {
            Id = Guid.NewGuid();
            Name = name;
            Prize = prize;
            Description = description;
        }

        [Key]
        public Guid Id { get; init; }
        public string Name { get; set; }
        public string? Description { get; set; }    
        public string? Prize { get; set; }
    }
}
