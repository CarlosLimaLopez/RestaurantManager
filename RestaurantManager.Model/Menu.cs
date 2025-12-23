using System.ComponentModel.DataAnnotations;

namespace RestaurantManager.Restaurant
{
    public class Menu
    {
        private Menu() { }

        public Menu(IEnumerable<MenuSection> sections, string note)
        {
            Id = Guid.NewGuid();
            Sections = sections;
            Note = note;
        }

        [Key]
        public Guid Id { get; init; }
        public IEnumerable<MenuSection> Sections{ get; set; }
        public string Note { get; set; }
    }
}
