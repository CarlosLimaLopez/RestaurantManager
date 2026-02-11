using System.ComponentModel.DataAnnotations;

namespace RestaurantManager.Menus
{
    using MenuSections;

    public class Menu
    {
        public Menu() { }

        public Menu(List<MenuSection> sections, DateOnly activateAt, string? note = null, string? nameSectionColor = null)
        {
            ActivateAt = activateAt;
            Sections = sections;
            Note = note;
            NameSectionColor = nameSectionColor;
        }

        [Key]
        public Guid Id { get; init; }
        public DateOnly ActivateAt { get; set; }
        public List<MenuSection> Sections { get; set; } = [];
        public string? Note { get; set; } = null;
        public string? NameSectionColor { get; set; } = null;

        public void AddMenuSection(MenuSection section) => Sections.Add(section);
    }
}
