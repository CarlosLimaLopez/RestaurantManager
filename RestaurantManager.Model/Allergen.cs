namespace RestaurantManager.Restaurant
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

    public static class Allergens
    {
        public static Allergen Fish => new(AllergenType.Fish, "Pescado", "fish");
        public static Allergen Lactose => new(AllergenType.Lactose, "Lactosa", "glass-water-droplet");
        public static Allergen Soy => new(AllergenType.Soy, "Soja", "leaf");
        public static Allergen TreeNuts => new(AllergenType.TreeNuts, "Frutos secos", "cubes-stacked");
        public static Allergen Crustaceans => new(AllergenType.Crustaceans, "Crustáceos", "shrimp");
        public static Allergen Mollusks => new(AllergenType.Mollusks, "Moluscos", "m");
        public static Allergen Sulfites => new(AllergenType.Sulfites, "Sulfitos", "flask");
        public static Allergen Gluten => new(AllergenType.Gluten, "Gluten", "wheat-awn");
        public static Allergen Sesame => new(AllergenType.Sesame, "Sésamo", "seedling");
        public static Allergen Eggs => new(AllergenType.Eggs, "Huevos", "egg");
        public static Allergen Celery => new(AllergenType.Celery, "Apio", "a");
        public static Allergen Mustard => new(AllergenType.Mustard, "Mostaza", "jar");

        public static IEnumerable<Allergen> GetAll()
        {
            return
            [
                Fish, Lactose, Soy, TreeNuts, Crustaceans,
                Mollusks, Sulfites, Gluten, Sesame, Eggs,
                Celery, Mustard
            ];
        }

        public static Allergen? GetByEnum(AllergenType type)
            => GetAll().FirstOrDefault(a => a.Type == type);            
        
        public static IEnumerable<Allergen> GetByEnumList(IEnumerable<AllergenType> types)
            => GetAll().Where(a => types.Contains(a.Type)).ToArray();
        
    }
}
