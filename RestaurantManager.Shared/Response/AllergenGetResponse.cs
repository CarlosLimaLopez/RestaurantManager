namespace RestaurantManager.Allergens
{
    public record AllergenGetResponse : IEquatable<AllergenGetResponse>
    {
        public string Type { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Icon { get; set; } = string.Empty;
    }
}
