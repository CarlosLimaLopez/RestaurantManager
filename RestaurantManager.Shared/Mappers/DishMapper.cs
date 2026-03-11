using RestaurantManager.Allergens;

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

        public static DishGetResponse ToResponse(this Dish dish)
        {
            return new DishGetResponse
            {
                DishId = dish.Id,
                Name = dish.Name,
                Order = dish.Order,
                Description = dish.Description,
                Prize = dish.Prize,
                Allergens = dish.Allergens?.Select(a => a.ToResponse()).ToList() ?? [],
                HasImage = dish.HasImage
            };
        }
    }
}
