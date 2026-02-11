namespace RestaurantManager.Dishes
{
    using Allergens;

    public class DishGetResponse
    {
        public Guid DishId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Order { get; set; }
        public string? Description { get; set; } = null;
        public string? Prize { get; set; } = null;
        public List<AllergenGetResponse> Allergens { get; set; } = [];
    }
}
