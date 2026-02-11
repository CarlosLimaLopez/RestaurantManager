namespace RestaurantManager.Allergens
{
    public enum AllergenType
    {
        Fish,
        Lactose,
        Soy,
        TreeNuts,
        Crustaceans,
        Mollusks,
        Sulfites,
        Gluten,
        Sesame,
        Eggs,
        Celery,
        Mustard
    }

    public class Allergen
    {
        public Allergen(AllergenType type, string name, string icon)
        {
            Type = type;
            Name = name;
            Icon = icon;
        }

        public AllergenType Type { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
    }
}
