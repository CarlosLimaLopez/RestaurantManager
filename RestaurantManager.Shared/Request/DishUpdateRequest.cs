namespace RestaurantManager.Dishes
{
    using Allergens;

    public class DishUpdateRequest
    {
        public string Name { get; set; } = string.Empty;
        public int Order { get; set; }
        public string? Description { get; set; } = null;
        public string? Prize { get; set; } = null;
        public IEnumerable<string> Allergens { get; set; } = [];
    }
}
