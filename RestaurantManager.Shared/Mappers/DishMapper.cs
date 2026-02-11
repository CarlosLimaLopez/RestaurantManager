namespace RestaurantManager.Dishes
{
    public static class DishMapper
    {
        public static DishUpdateRequest ToDishUpdateRequest(this DishGetResponse dish)
        {
            return new DishUpdateRequest
            {
                Name = dish.Name,
                Order = dish.Order,
                Description = dish.Description,
                Prize = dish.Prize,
                Allergens = dish.Allergens.Select(a => a.Type).ToArray()
            };
        }
    }
}
