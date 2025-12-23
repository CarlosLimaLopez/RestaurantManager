using System.ComponentModel.DataAnnotations;

namespace RestaurantManager.Restaurant
{
    public class Restaurant
    {
        private Restaurant() { }

        public Restaurant(string name, Menu menu)
        {
            Id = Guid.NewGuid();
            Name = name;
            Menu = menu;
        }

        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Menu Menu { get; set; }
    }
}
