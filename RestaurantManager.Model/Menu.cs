using System.ComponentModel.DataAnnotations;

namespace RestaurantManager.Restaurant
{
    public class Menu
    {
        private Menu() { }

        public Menu(IEnumerable<MenuSection> sections, DateOnly activateAt, string? note = null, string? nameSectionColor = null)
        {
            Id = Guid.NewGuid();
            ActivateAt = activateAt;
            Sections = sections;
            Note = note;
            NameSectionColor = nameSectionColor;
        }

        [Key]
        public Guid Id { get; init; }
        public DateOnly ActivateAt { get; init; }
        public IEnumerable<MenuSection> Sections{ get; set; }
        public string? Note { get; set; }
        public string? NameSectionColor { get; set; }
    }
}
